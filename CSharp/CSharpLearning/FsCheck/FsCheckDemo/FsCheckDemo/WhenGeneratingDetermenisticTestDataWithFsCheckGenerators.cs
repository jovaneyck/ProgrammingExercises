using FsCheck;
using Xunit;

namespace FsCheckDemo
{
    public class WhenGeneratingDetermenisticTestDataWithFsCheckGenerators
    {
        [Fact]
        public void DetermenisticDataWhenSeeded()
        {
            var aNumber = Arb.Generate<int>().Eval(100, Random.mkStdGen(1447));
            Assert.Equal(-86, aNumber);
        }

        [Fact]
        public void AllowsToNarrowDownDomainOfAType()
        {
            var aNumber = Arb.Generate<int>()
                .Where(n => n > 0 && n % 2 == 0) //only even, positive numbers please
                .Eval(100, Random.mkStdGen(1447));
            Assert.Equal(20, aNumber);
        }

        [Fact]
        public void LimitedSetOfOptions_CombiningGenerators()
        {
            var generator = Gen.OneOf(Gen.Constant(1337), Gen.Constant(666));
            var aNumber = generator.Eval(1, Random.mkStdGen(1000));

            Assert.Equal(666, aNumber);
        }

        class WithProperties
        {
            public int A { get; set; }
            public string B { get; set; }
            public int C { get; set; }
            public int D { get; set; }
        }

        [Fact]
        public void SetsPublicPropertiesOfAGeneratedTypeByDefault()
        {
            var anObject = Arb.Generate<WithProperties>().Eval(10, Random.mkStdGen(1337));
            Assert.Equal(1, anObject.A);
            Assert.Equal("G", anObject.B);
        }

        class WithConstructor
        {
            public readonly int A;

            public WithConstructor(int a)
            {
                A = a;
            }
        }

        [Fact]
        public void CanInstantiateTypesWithAConstructorExpectingArguments()
        {
            var anObject = Arb.Generate<WithConstructor>().Eval(500, Random.mkStdGen(1337));
            Assert.Equal(-344, anObject.A);
        }

        [Fact]
        public void BuildingComplexTypesWithControlOverIndividualProperties()
        {
            var generator =
                //Does not really scale for N properties, but could pass through the constructor just fine.
                from a in Gen.Elements(1337, 666)
                from b in Gen.Constant("hello world")
                select new WithProperties { A = a, B = b };
            var anObject = generator.Eval(10, Random.mkStdGen(1337));

            Assert.Equal(666, anObject.A);
            Assert.Equal("hello world", anObject.B);
        }

        [Fact]
        public void BuildingComplexTypesWithControlOverIndividualProperties_Scalable()
        {
            var generator =
                //Scales a bit better but is IMPURE as hell
                from generatedObject in Arb.Generate<WithProperties>() //generates a completely arbitrary object
                from generatedString in Gen.Elements("hello world", "OHAI") //generate something with more control
                select new { generatedObject, generatedString };

            var impureGenerator = generator.Select(g =>
            {
                //impure side-effect, breaking all kinds of applicative/functor/monadic laws.
                g.generatedObject.B = g.generatedString;
                g.generatedObject.C = 666;
                return g.generatedObject;
            });

            var anObject = impureGenerator.Eval(10000, Random.mkStdGen(1337));

            Assert.Equal(-783, anObject.A);
            Assert.Equal("OHAI", anObject.B);
            Assert.Equal(666, anObject.C);
            Assert.Equal(-1306, anObject.D);
        }
    }
}

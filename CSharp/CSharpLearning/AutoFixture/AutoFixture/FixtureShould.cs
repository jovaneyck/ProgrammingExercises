using System.Diagnostics;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoFixture
{
    public class FixtureShould
    {
        [Fact]
        public void GenerateAnonymousDataForBasicTypes()
        {
            var fixture = new Fixture();

            Assert.True(fixture.Create<int>() > 0);
            Assert.NotNull(fixture.Create<string>());
        }

        [Fact]
        public void GenerateRandomDataBasedOnASeed()
        {
            var fixture = new Fixture();
            Assert.True(fixture.Create("start").StartsWith("start"));
        }

        public class Bar
        {
            public string String { get; set; }
        }

        [Fact]
        public void CreateAnonymousValuesForCustomTypes()
        {
            var fixture = new Fixture();
            Assert.NotNull(fixture.Create<Bar>().String);
        }

        public class Foo
        {
            public int Integer { get; set; }
            public Bar Bar { get; set; }
        }

        [Fact]
        public void GenerateObjectGraphs()
        {
            var fixture = new Fixture();
            var anonymousFoo = fixture.Create<Foo>();

            Assert.NotNull(anonymousFoo.Bar);
            Assert.NotNull(anonymousFoo.Bar.String);
        }

        [Fact]
        public void GenerateSequences()
        {
            var fixture = new Fixture();
            var numbers = fixture.CreateMany<int>();
            Assert.True(numbers.Any());
        }

        [Fact]
        public void GenerateSequencesOfASpecifiedLength()
        {
            var fixture = new Fixture();
            var numbers = fixture.CreateMany<int>(666);
            Assert.Equal(666, numbers.Count());
        }

        [Fact]
        public void AddGeneratedSequencesToAnExistingSequence()
        {
            var fixture = new Fixture();
            var numbers = new List<int>();

            fixture.AddManyTo(numbers); //beware, don't break encapsulation for this!

            Assert.True(numbers.Any());
        }

        [Fact]
        public void MakeUseOfSuppliedCreatorFunctions()
        {
            var fixture = new Fixture();
            var numbers = new List<int>();
            fixture.AddManyTo(numbers, () => 42);

            Assert.True(numbers.All(n => n == 42));
        }

        public class IntegerCollection
        {
            public List<int> Integers { get; set; }
        }

        [Fact]
        public void PopulateListsOfComplexTypes()
        {
            var fixture = new Fixture();
            Assert.True(fixture.Create<IntegerCollection>().Integers.Any());
        }

        public class Qux
        {
            public string Message { get; private set; } //A private field set through the constructor
            public object PrivateFieldNotSetThroughConstructor { get; private set; } //Not set through the constructor!

            public Qux(string message) //No parameterless constructor!
            {
                Message = message;
            }
        }

        [Fact]
        public void GenerateCustomTypesWithoutBreakingEncapsulation()
        {
            var fixture = new Fixture();
            var anonymousQux = fixture.Create<Qux>();
            Assert.NotNull(anonymousQux.Message);
            Assert.Null(anonymousQux.PrivateFieldNotSetThroughConstructor); //AutoFixture does not initialize private fields that are not initialized through ctor :(
        }
    }
}

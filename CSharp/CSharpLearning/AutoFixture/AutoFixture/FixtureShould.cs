using System.Diagnostics;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;

namespace AutoFixture
{
    [TestFixture]
    public class FixtureShould
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void GenerateAnonymousDataForBasicTypes()
        {
          
            Assert.IsTrue(_fixture.Create<int>() > 0);
            Assert.IsNotNull(_fixture.Create<string>());
        }

        [Test]
        public void GenerateRandomDataBasedOnASeed()
        {
            Assert.IsTrue(_fixture.Create("start").StartsWith("start"));
        }

        public class Bar
        {
            public string String { get; set; }
        }

        [Test]
        public void CreateAnonymousValuesForCustomTypes()
        {
            Assert.IsNotNull(_fixture.Create<Bar>().String);
        }

        public class Foo
        {
            public int Integer { get; set; }
            public Bar Bar { get; set; }
        }

        [Test]
        public void GenerateObjectGraphs()
        {
            var anonymousFoo = _fixture.Create<Foo>();

            Assert.IsNotNull(anonymousFoo.Bar);
            Assert.IsNotNull(anonymousFoo.Bar.String);
        }

        [Test]
        public void GenerateSequences()
        {
            var numbers = _fixture.CreateMany<int>();
            Assert.IsTrue(numbers.Any());
        }

        [Test]
        public void GenerateSequencesOfASpecifiedLength()
        {
            var numbers = _fixture.CreateMany<int>(666);
            Assert.AreEqual(666, numbers.Count());
        }

        [Test]
        public void AddGeneratedSequencesToAnExistingSequence()
        {
            var numbers = new List<int>();

            _fixture.AddManyTo(numbers); //beware, don't break encapsulation for this!

            Assert.IsTrue(numbers.Any());
        }

        [Test]
        public void MakeUseOfSuppliedCreatorFunctions()
        {
            var numbers = new List<int>();
            _fixture.AddManyTo(numbers, () => 42);

            Assert.IsTrue(numbers.All(n => n == 42));
        }

        public class IntegerCollection
        {
            public List<int> Integers { get; set; }
        }

        [Test]
        public void PopulateListsOfComplexTypes()
        {
            Assert.IsTrue(_fixture.Create<IntegerCollection>().Integers.Any());
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

        [Test]
        public void GenerateCustomTypesWithoutBreakingEncapsulation()
        {
            var anonymousQux = _fixture.Create<Qux>();
            Assert.IsNotNull(anonymousQux.Message);
            Assert.IsNull(anonymousQux.PrivateFieldNotSetThroughConstructor); //AutoFixture does not initialize private fields that are not initialized through ctor :(
        }
    }
}

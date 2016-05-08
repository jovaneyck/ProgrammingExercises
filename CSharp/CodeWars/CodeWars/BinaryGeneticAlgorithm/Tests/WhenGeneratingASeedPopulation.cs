using System.Linq;
using NUnit.Framework;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class WhenGeneratingASeedPopulation
    {
        private readonly RandomPopulationGenerator _generator;

        public WhenGeneratingASeedPopulation()
        {
            _generator = new RandomPopulationGenerator(new RandomBitGenerator(new Randomizer()));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(13)]
        public void CreatesTheCorrectAmountOfChromosones(int numberOfChromosones)
        {
            Assert.AreEqual(numberOfChromosones, _generator.CreatePopulationOf(numberOfChromosones, 1).Count);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(35)]
        public void ChromosonesHaveTheRequiredLength(int chromosoneLength)
        {
            Assert.AreEqual(chromosoneLength, _generator.CreatePopulationOf(1, chromosoneLength).Single().Length());
        }

        [Test]
        public void ChromosonesAreRandom()
        {
            var generatedChromosone = _generator.CreatePopulationOf(1, 100).Single();

            Assert.Contains('1', generatedChromosone.ToString().ToCharArray());
            Assert.Contains('0', generatedChromosone.ToString().ToCharArray());
        }

        [Test]
        public void ChromosonesAreDifferent()
        {
            var generatedChromosones = _generator.CreatePopulationOf(2, 100);
            Assert.AreNotEqual(generatedChromosones.ElementAt(0).ToString(), generatedChromosones.ElementAt(1).ToString());
        }
    }
}
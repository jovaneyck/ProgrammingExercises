using System.Collections.Generic;
using NUnit.Framework;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class RatedPopulationTests
    {
        [Test]
        public void KnowsWhenItContainsAPerfectChromosone()
        {
            var population = new RatedPopulation(new []
            {
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 0.33),
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 1),
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 0.75)
            });
            Assert.IsTrue(population.ContainsPerfectChromosone());
        } 

        [Test]
        public void KnowsWhenItDoesNotContainAPerfectChromosone()
        {
            var population = new RatedPopulation(new []
            {
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 0.33),
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 0.666),
                new ChromosoneWithFitness(new Chromosone(new List<int>()), 0.75)
            });
            Assert.IsFalse(population.ContainsPerfectChromosone());
        } 
    }
}
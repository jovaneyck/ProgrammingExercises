using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Moq;
using NUnit.Framework;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class WhenRunningTheGeneticAlgorithm
    {
        [Test]
        public void AsksForAPopulationOfTheCorrectLength()
        {
            var populationGenerator = new Mock<PopulationGenerator>();
            populationGenerator
                .Setup(generator => generator.CreatePopulationOf(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new [] {new Chromosone(new List<int>()) });

            var algorithm = new GeneticAlgorithm(populationGenerator.Object, Mock.Of<ParentSelector>());

            algorithm.Run(
                (_ => 1), 
                3, 
                0, 
                0, 
                1);

            populationGenerator
                .Verify(generator => generator.CreatePopulationOf(algorithm.POPULATION_SIZE, 3));
        }
    }
}
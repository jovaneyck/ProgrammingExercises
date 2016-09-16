using NUnit.Framework;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class WhenEvaluatingTheSecretNumberFitnessFunction
    {
        [TestCase("1", "1", 1)]
        [TestCase("0", "1", 0)]
        [TestCase("11", "11", 1)]
        [TestCase("11", "01", 0.5)]
        [TestCase("10", "01", 0)]
        [TestCase("1010", "1011", 0.75)]
        [TestCase("1010", "1101", 0.25)]
        public void WorksAsExpected(string input, string secretNumber, double expectedFitness)
        {
            Assert.AreEqual(expectedFitness, SecretNumber.DistanceToSecretNumber(input, secretNumber));
        }
    }
}
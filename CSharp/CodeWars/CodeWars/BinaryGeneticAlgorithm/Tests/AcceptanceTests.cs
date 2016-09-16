using NUnit.Framework;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class AcceptanceTests
    {
        [Test]
        public void AcceptanceTest_CanFindASecretNumber()
        {
            var secretNumber = "10010111011001011101100101110110011";
            var result =
                new GeneticAlgorithm().Run(
                    number => SecretNumber.DistanceToSecretNumber(number, secretNumber),
                    secretNumber.Length,
                    0.6,
                    0.002,
                    100);

            Assert.AreEqual(secretNumber, result);
        }
    }
}
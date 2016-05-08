using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm.Tests
{
    public class SecretNumber
    {
        public static double DistanceToSecretNumber(string input, string secretNumber)
        {
            var inputChromosone = AsChromosone(input);
            var secretChromosone = AsChromosone(secretNumber);

            return Distance(inputChromosone, secretChromosone);
        }

        private static IList<int> AsChromosone(string input)
        {
            return input
                .ToCharArray()
                .Select(bit => bit == '1' ? 1 : 0)
                .ToList();
        }

        private static double Distance(IList<int> aChromosone, IEnumerable<int> anotherChromosone)
        {
            var numberOfIdenticalBits =
                aChromosone
                    .Zip(anotherChromosone,
                        (x, y) => new { first = x, second = y })
                    .Count(tuple => tuple.first == tuple.second);

            return numberOfIdenticalBits / (double)aChromosone.Count();
        }
    }
}
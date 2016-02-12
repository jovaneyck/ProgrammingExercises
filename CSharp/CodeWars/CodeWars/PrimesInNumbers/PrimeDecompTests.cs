using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace CodeWars.PrimesInNumbers
{
    [TestFixture]
    public class PrimeDecompTests
    {
        [TestCase(2, new [] { 2 })]
        [TestCase(4, new [] { 2, 3 })]
        [TestCase(7, new [] { 2,3,5,7 })]
        [TestCase(8, new [] { 2,3,5,7 })]
        public void CanFindAllPrimeNumbers(int n, int [] expectedPrimes)
        {
            Assert.AreEqual(expectedPrimes, PrimeDecomp.PrimesSmallerOrEqualThan(n).ToArray());
        }

        [Test]
        public void LazilyCalculatesNeededPrimes()
        {
            var stopwatch = Stopwatch.StartNew();

            Assert.NotNull(PrimeDecomp.PrimesSmallerOrEqualThan(7775460).Take(50).ToList());

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Assert.True(stopwatch.ElapsedMilliseconds < 3000);
        }

        [TestCase(2,"(2)")]
        [TestCase(4,"(2**2)")]
        [TestCase(12,"(2**2)(3)")]
        [TestCase(86240, "(2**5)(5)(7**2)(11)")] //Acceptance test
        [TestCase(7775460, "(2**2)(3**3)(5)(7)(11**2)(17)")] //Acceptance test
        public void DecomposesANumberInPrimeFactors(int number, string expectedDecomposition)
        {
            Assert.AreEqual(expectedDecomposition, PrimeDecomp.factors(number));
        }

        [TestCase(2, new[] {2})]
        [TestCase(3, new[] {3})]
        [TestCase(6, new[] {2,3})]
        [TestCase(10, new[] {2,5})]
        [TestCase(12, new[] {2,2,3})]
        public void DecomposesANumberInPrimeFactors(int number, int[] expected)
        {
            Assert.AreEqual(expected, PrimeDecomp.FactorDecomposition(number));
        }

        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(5, true)]
        [TestCase(7, true)]
        [TestCase(1, false)]
        [TestCase(4, false)]
        [TestCase(8, false)]
        [TestCase(9, false)]
        public void RecognizesPrimes(int number, bool isPrime)
        {
            Assert.AreEqual(isPrime, PrimeDecomp.IsPrime(number));
        }
    }

    public class PrimeDecomp
    {
        public static string factors(int number)
        {
            return
                string.Join(
                    "",
                    FactorDecomposition(number)
                        .GroupBy(n => n)
                        .OrderBy(g => g.Key)
                        .Select(n => new {factor = n.Key, occurences = n.Count()})
                        .Select(f =>
                            f.occurences == 1 
                                ? $"({f.factor})" 
                                : $"({f.factor}**{f.occurences})"));

        }


        public static IList<int> FactorDecomposition(int number)
        {
            var result = new List<int>();

            while (!IsPrime(number))
            {
                foreach (var f in PrimesSmallerOrEqualThan(number))
                {
                    if (number%f == 0)
                    {
                        result.Add(f);
                        number = number/f;
                        break;
                    }
                }
            }

            result.Add(number);

            return result;
        }

        public static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            return Enumerable.Range(2, number-2).All(n => number % n != 0);
        }

        /*
            Sieve of Eratosthenes:
            To find all the prime numbers less than or equal to a given integer n by Eratosthenes' method:

            Create a list of consecutive integers from 2 through n: (2, 3, 4, ..., n).
            Initially, let p equal 2, the smallest prime number.
            Enumerate the multiples of p by counting to n from 2p in increments of p, and mark them in the list (these will be 2p, 3p, 4p, ... ; the p itself should not be marked).
            Find the first number greater than p in the list that is not marked. If there was no such number, stop. Otherwise, let p now equal this new number (which is the next prime), and repeat from step 3.
        */
        public static IEnumerable<int> PrimesSmallerOrEqualThan(int number)
        {
            var possiblePrimes = Enumerable.Range(2, number - 1);

            var smallestPrime = 2;
            
            while (smallestPrime != default(int))
            {
                yield return smallestPrime;
                var sp = smallestPrime;
                possiblePrimes = possiblePrimes.Where(pp => pp % sp != 0);
                smallestPrime = possiblePrimes.FirstOrDefault();
            }
        }
    }
}
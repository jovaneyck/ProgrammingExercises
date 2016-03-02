﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeWars.NextBiggestNumber
{
    [TestFixture]
    public class NextBiggerNumberTests
    {
        [TestCase(12,21)]
        [TestCase(513,531)]
        [TestCase(2017,2071)]
        [TestCase(414,441)]
        [TestCase(144,414)]
        public void CalculatesTheNextBiggestNumber(long input, long nextBiggerNumber)
        {
            Assert.AreEqual(nextBiggerNumber, Kata.NextBiggerNumber(input));
        }

        [TestCase(9)]
        [TestCase(91)]
        [TestCase(99)]
        public void ReturnsMinusOneWhenNoBiggerNumberPossible(long input)
        {
            Assert.AreEqual(-1, Kata.NextBiggerNumber(input));
        }
    }

    public class Kata
    {
        public static long NextBiggerNumber(long number)
        {
            var digits = Digits(number);
            var allCombinations = AllPermutations(digits).Select(ToNumber);
            return NextBigger(number, allCombinations).GetValueOrDefault(-1);
        }

        private static long ToNumber(IList<long> nextBigger)
        {
            return long.Parse(string.Join("", nextBigger));
        }

        private static long? NextBigger(long startingNumber, IEnumerable<long> allNumbers)
        {
            return allNumbers
                .Select(n => new long?(n))
                .OrderBy(n=>n)
                .FirstOrDefault(n => n > startingNumber);
        }

        private static IEnumerable<IList<long>> AllPermutations(IList<long> digits)
        {
            if (digits.Count == 1)
            {
               return new [] { digits };
            }

            return PermutationsRecursive(digits);
        }

        private static IEnumerable<IList<long>> PermutationsRecursive(IList<long> digits)
        {
            var first = digits.First();
            var permutationsWithoutFirstElement = AllPermutations(digits.Skip(1).ToList());

            foreach (var permutationWithoutFirstElement in permutationsWithoutFirstElement)
            {
                for (var index = 0; index <= permutationWithoutFirstElement.Count; index++)
                {
                    var aPermutation = permutationWithoutFirstElement.ToList();
                    aPermutation.Insert(index, first);
                    yield return aPermutation;
                }
            }
        }

        private static IList<long> Digits(long number)
        {
            return number.ToString()
                .ToCharArray()
                .ToList()
                .Select(c => $"{c}")
                .Select(long.Parse)
                .ToList();
        } 
    }
}
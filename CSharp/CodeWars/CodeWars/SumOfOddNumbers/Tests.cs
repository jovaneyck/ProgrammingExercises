using System;
using System.Collections.Generic;
using System.Linq;
using static System.Int32;
using NUnit.Framework;

namespace CodeWars.SumOfOddNumbers
{

    [TestFixture]
    public class Test
    {
        [TestCase(1,1)]
        [TestCase(2,8)]
        [TestCase(3,27)]
        [TestCase(4,64)]
        [TestCase(42,74088)]
        public void CalculatesSumOfOddRumbers(int input, int expected)
        {
            Assert.AreEqual(expected, Kata.rowSumOddNumbers(input));
        }

    }

    /*
                         1
                      3     5
                   7     9    11
               13    15    17    19
            21    23    25    27    29
        31     33    35    37    39    41   
    */
    public class Kata
    {
        public static int rowSumOddNumbers(long rowNumber)
        {
            //Or Math.Pow(rowNumber, 3) :)
            return Odds()
                .Skip(NaturalNumbers().Take((int)(rowNumber - 1)).Sum())
                .Take((int)rowNumber)
                .Sum();
        }

        private static IEnumerable<int> NaturalNumbers()
        {
            return Enumerable.Range(1, MaxValue);
        }

        private static IEnumerable<int> Odds()
        {
            return NaturalNumbers().Where(n => n%2 != 0);
        }
    }
}
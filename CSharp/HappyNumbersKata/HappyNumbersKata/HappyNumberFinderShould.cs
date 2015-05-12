using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Xunit;

namespace HappyNumbersKata
{
    public class HappyNumberFinderShould
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(31, true)] //31 -> 10 -> 1
        //non-trivial happy (31)
        //non-trivial non-happy (4)
        public void RecognizeHappyNumbers(int numberToCheck, bool isHappyNumber)
        {
            Assert.Equal(isHappyNumber, new HappyNumberFinder().IsHappy(numberToCheck));
        }         
    }

    internal class HappyNumberFinder
    {
        public bool IsHappy(int number)
        {
            if (number == 1)
            {
                return true;
            }
            if (number == 0)
            {
                return false;
            }

            var digits = ToDigits(number);
            var squaredDigits = digits.Select(d=> (int)Math.Pow(d, 2));
            var nextCandidate = squaredDigits.Sum();

            return IsHappy(nextCandidate);
        }

        private IEnumerable<int> ToDigits(int number)
        {
            var result = new List<int>();
            while (number != 0)
            {
                result.Add(number % 10);
                number = number/10;
            }

            return result;
        }
    }
}
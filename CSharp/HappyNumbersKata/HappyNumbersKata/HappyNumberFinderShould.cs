using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HappyNumbersKata
{
    public class HappyNumberFinderShould
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(31, true)] //31 -> 10 -> 1
        [InlineData(4, false)] //4 -> 16 -> 37 -> 58 -> 89 -> 145 -> 42 -> 20 -> 4
        public void RecognizeHappyNumbers(int numberToCheck, bool isHappyNumber)
        {
            Assert.Equal(isHappyNumber, new HappyNumberFinder().IsHappy(numberToCheck));
        }         
    }

    internal class HappyNumberFinder
    {
        public bool IsHappy(int number)
        {
            return IsHappyWithKnownUnhappyNumbers(number, new List<int>());
        }

        private bool IsHappyWithKnownUnhappyNumbers(int number, ICollection<int> knownUnhappyNumbers)
        {
            while (true)
            {
                if (number == 1)
                {
                    return true;
                }

                if (number == 0)
                {
                    return false;
                }

                if (knownUnhappyNumbers.Contains(number))
                {
                    return false;
                }

                knownUnhappyNumbers.Add(number);

                var digits = ToDigits(number);
                var squaredDigits = digits.Select(d => (int) Math.Pow(d, 2));
                var nextCandidate = squaredDigits.Sum();

                number = nextCandidate;
            }
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RomanNumerals
{
    [TestFixture]
    class RomanTest
    {
        [Test]
        public void One()
        {
            Verify(1, "I");
        }

        private void Verify(int number, string expectedResult)
        {
            Assert.AreEqual(expectedResult, ToRoman(number));
        }

        [Test]
        public void Two()
        {
            Verify(2,"II");
        }

        [Test]
        public void Three()
        {
            Verify(3, "III");
        }

        [Test]
        public void Four()
        {
            Verify(4, "IV");
        }

        [Test]
        public void Five()
        {
            Verify(5, "V");
        }

        [Test]
        public void Six()
        {
            Verify(6, "VI");
        }

        [Test]
        public void Seven()
        {
            Verify(7, "VII");
        }

        [Test]
        public void Nine()
        {
            Verify(9, "IX");
        }

        [Test]
        public void Ten()
        {
            Verify(10, "X");
        }

        [Test]
        public void Eleven()
        {
            Verify(11, "XI");
        }

        private string ToRoman(int number)
        {
            if (number == 0)
                return "";

            IDictionary<int, string> romanLiterals =
                new Dictionary<int, string>()
                {
                    {1, "I"},
                    {4, "IV"},
                    {5, "V"},
                    {9, "IX"},
                    {10, "X"}
                };

            var specialNumbersFromHighToLow = romanLiterals.Keys.OrderBy(x=>-x);
            foreach(int specialNumber in specialNumbersFromHighToLow)
                if(number >= specialNumber)
                    return romanLiterals[specialNumber] + ToRoman(number - specialNumber);

            throw new ArgumentException("Could not transform the given number", "number");
        }
    }
}

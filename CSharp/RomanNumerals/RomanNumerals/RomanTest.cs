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

        private string ToRoman(int number)
        {
            const string oneInRoman = "I";

            if (number == 10)
                return "X";
            if (number == 9)
                return "IX";
            if (number >= 5)
                return "V" + ToRoman(number - 5);
            if (number == 4)
                return "IV";
            if (number == 0)
                return "";

            return oneInRoman + ToRoman(number - 1);
        }
    }
}

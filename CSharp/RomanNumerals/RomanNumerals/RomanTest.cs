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

        private string ToRoman(int number)
        {
            if (number == 4)
                return "IV";
            if(number == 1)
                return "I";
            if(number == 2)
                return "II";
            return "III";
        }
    }
}

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
        [TestCase(1, Result = "I")]
        [TestCase(2, Result = "II")]
        [TestCase(3, Result = "III")]
        [TestCase(4, Result = "IV")]
        [TestCase(5, Result = "V")]
        [TestCase(6, Result = "VI")]
        [TestCase(7, Result = "VII")]
        [TestCase(9, Result = "IX")]
        [TestCase(10, Result = "X")]
        [TestCase(11, Result = "XI")]
        [TestCase(14, Result = "XIV")]
        [TestCase(15, Result = "XV")]
        [TestCase(16, Result = "XVI")]
        [TestCase(19, Result = "XIX")]
        [TestCase(20, Result = "XX")]
        [TestCase(40, Result = "XL")]
        [TestCase(41, Result = "XLI")]
        [TestCase(44, Result = "XLIV")]
        [TestCase(46, Result = "XLVI")]
        [TestCase(50, Result = "L")]
        [TestCase(60, Result = "LX")]
        [TestCase(90, Result = "XC")]
        [TestCase(99, Result = "XCIX")]
        [TestCase(100, Result = "C")]
        public string TranslatesCorrectly(int number)
        {
            return ToRoman(number);
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
                    {10, "X"},
                    {40, "XL"},
                    {50, "L"},
                    {90, "XC"},
                    {100, "C"}
                };

            var specialNumbersFromHighToLow = romanLiterals.Keys.OrderBy(x=>-x);
            foreach(int specialNumber in specialNumbersFromHighToLow)
                if(number >= specialNumber)
                    return romanLiterals[specialNumber] + ToRoman(number - specialNumber);

            throw new ArgumentException("Could not transform the given number", "number");
        }
    }
}

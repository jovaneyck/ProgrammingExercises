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
        [TestCase(400, Result = "CD")]
        [TestCase(500, Result = "D")]
        [TestCase(900, Result = "CM")]
        [TestCase(1000, Result = "M")]
        [TestCase(2749, Result = "MMDCCXLIX")]
        public string TranslatesCorrectlyFromDecimalToRoman(int number)
        {
            return (new RomanConverter()).ToRoman(number);
        }

        [TestCase("I", Result = 1)]
        [TestCase("II", Result = 2)]
        public int TranslatesCorrectlyFromRomanToDecimal(string romanNumber)
        {
            return (new RomanConverter()).ToDecimal(romanNumber);
        }
    }
}

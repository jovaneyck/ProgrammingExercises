using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumerals
{
    public class RomanConverter
    {
        private readonly IDictionary<int, string> _literals =
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
                        {100, "C"},
                        {400, "CD"},
                        {500, "D"},
                        {900, "CM"},
                        {1000, "M"}
                    };


        public string ToRoman(int number)
        {
            if (number == 0)
                return "";

            var specialNumbersFromHighToLow = _literals.Keys.OrderBy(x => -x);
            foreach (int specialNumber in specialNumbersFromHighToLow)
                if (number >= specialNumber)
                    return _literals[specialNumber] + ToRoman(number - specialNumber);

            throw new ArgumentException("Could not transform the given number", "number");
        }

        public int ToDecimal(string s)
        {
            return 1;
        }
    }
}
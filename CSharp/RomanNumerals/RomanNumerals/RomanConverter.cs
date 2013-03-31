using System;
using System.Collections.Generic;
using System.Linq;

namespace RomanNumerals
{
    public class RomanConverter
    {
        private readonly IDictionary<int, string> _decimalToRomanLiterals =
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

        private IDictionary<string, int> _romanToDecimalLiterals;

        public RomanConverter()
        {
            InitializeReverseLookup();
        }

        private void InitializeReverseLookup()
        {
            _romanToDecimalLiterals = new Dictionary<string, int>();
            foreach (KeyValuePair<int, string> pair in _decimalToRomanLiterals)
                _romanToDecimalLiterals.Add(pair.Value, pair.Key);
        }

        public string ToRoman(int number)
        {
            if (number == 0)
                return "";

            var specialNumbersFromHighToLow = _decimalToRomanLiterals.Keys.OrderBy(x => -x);
            foreach (int specialNumber in specialNumbersFromHighToLow)
                if (number >= specialNumber)
                    return _decimalToRomanLiterals[specialNumber] + ToRoman(number - specialNumber);

            throw new ArgumentException("Could not transform the given number", "number");
        }

        public int ToDecimal(string romanNumber)
        {
            if (_romanToDecimalLiterals.ContainsKey(romanNumber))
                return _romanToDecimalLiterals[romanNumber];
            if(romanNumber.Equals("I"))
                return 1;
            if (romanNumber.Equals("II"))
                return 2;
            if (romanNumber.Equals("III"))
                return 3;

            throw new ArgumentException("Could not transform "+romanNumber+" to a decimal.");
        }
    }
}
using System;
using System.Collections.Generic;

namespace MontyHallKata
{
    public class PreCookedNumberGenerator : RandomNumberGenerator
    {
        private readonly List<double> numbers = new List<double>();
        private int currentNumberIndex = 0;


        public PreCookedNumberGenerator(string s)
        {
            String[] numbersAsStrings = s.Split(';');
            foreach(string numberAsString in numbersAsStrings)
                numbers.Add(Double.Parse(numberAsString));
        }

        public double Next()
        {
            return numbers[currentNumberIndex++];
        }
    }
}
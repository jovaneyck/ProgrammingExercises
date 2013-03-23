using System;

namespace RomanNumerals
{
    class Program
    {
        static void Main(string[] args)
        {
            RomanConverter converter = new RomanConverter();

            for(int number = 1; number <= 3000; number ++)
                Console.WriteLine(number + ": "+ converter.ToRoman(number));
            Console.ReadKey();
        }
    }
}

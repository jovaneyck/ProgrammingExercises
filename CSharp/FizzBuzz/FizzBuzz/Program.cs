using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz buzzer = new FizzBuzz();

            for(int number = 1; number <= 100; number++)
                Console.WriteLine(buzzer.FizzBuzzify(number));
            Console.ReadKey();
        }
    }
}

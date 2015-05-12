using System;
using System.Linq;

namespace HappyNumbersKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var finder = new HappyNumberFinder();

            var happyNumbers = Enumerable.Range(1, 100)
                .Select(n => new {Number = n, IsHappy = finder.IsHappy(n)})
                .Where(candidate => candidate.IsHappy)
                .Select(candidate => candidate.Number);

            foreach (var happyNumber in happyNumbers)
            {
                Console.WriteLine(happyNumber);
            }

            Console.ReadKey();
        }
    }
}

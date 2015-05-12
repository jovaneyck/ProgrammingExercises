using System;
using System.Diagnostics;
using System.Linq;

namespace HappyNumbersKata
{
    class Program
    {
        static void Main()
        {
            var finder = new HappyNumberFinder();
            var stopwatch = Stopwatch.StartNew();

            var happyNumbers = Enumerable.Range(1, 1000)
                .Select(n => new {Number = n, IsHappy = finder.IsHappy(n)})
                .Where(candidate => candidate.IsHappy)
                .Select(candidate => candidate.Number);

            stopwatch.Stop();

            foreach (var happyNumber in happyNumbers)
            {
                Console.WriteLine(happyNumber);
            }

            Console.WriteLine("Run time: {0} ms.", stopwatch.ElapsedMilliseconds);

            Console.ReadKey();
        }
    }
}

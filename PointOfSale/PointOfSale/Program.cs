using System;

namespace PointOfSale
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new ConsoleDisplay();

            display.DisplayPrice(new Price(133750));
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();
        }
    }
}

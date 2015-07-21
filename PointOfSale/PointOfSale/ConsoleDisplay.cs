using System;

namespace PointOfSale
{
    public class ConsoleDisplay
    {
        public void DisplayPrice(Price price)
        {
            Console.WriteLine(price.Formatted());
        }
    }
}
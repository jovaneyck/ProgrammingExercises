using System;

namespace ContractTestDemo
{
    public class ConsoleDisplay : Display
    {
        public void DisplayPrice(int price)
        {
            Console.WriteLine(price);
        }
    }
}
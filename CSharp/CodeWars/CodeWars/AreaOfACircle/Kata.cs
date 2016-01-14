using System;
using System.Globalization;
using static System.Double;

namespace CodeWars.AreaOfACircle
{
    public class Kata
    {
        public static double CalculateAreaOfCircle(string radius)
        {
            double radiusAsNumber;
            if (! TryParse(radius, out radiusAsNumber) || radiusAsNumber <= 0)
            {
                throw new ArgumentException();
            }

            return 
                Math.Round(
                    Math.PI * Math.Pow(radiusAsNumber, 2),
                    2);
        }

        private static bool TryParse(string radius, out double radiusAsNumber)
        {
            return double.TryParse(
                radius, 
                NumberStyles.AllowDecimalPoint, 
                new NumberFormatInfo {NumberDecimalSeparator = "."}, 
                out radiusAsNumber);
        }
    }
}
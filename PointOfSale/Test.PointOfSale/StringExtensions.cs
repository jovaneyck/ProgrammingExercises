using System;

namespace Test.PointOfSale
{
    public static class StringExtensions
    {
        public static string[] Lines(this string theString)
        {
            return theString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
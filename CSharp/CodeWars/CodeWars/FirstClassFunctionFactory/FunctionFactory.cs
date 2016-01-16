using System;
using System.Linq;

namespace CodeWars.FirstClassFunctionFactory
{
    public class FunctionFactory
    {
        public static Func<int[], int[]> factory(int multiplier) =>
            numbers => numbers.Select(n => n * multiplier).ToArray();
    }
}
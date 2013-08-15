using System;

namespace MontyHallKata
{
    public class TrulyRandomNumberGenerator : RandomNumberGenerator
    {
        readonly Random random = new Random();

        public double Next()
        {
            return random.NextDouble();
        }
    }
}
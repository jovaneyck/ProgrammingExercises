using System;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class RandomBitGenerator : BitGenerator
    {
        private readonly Random _rng;

        public RandomBitGenerator(Random randomNumberGenerator)
        {
            _rng = randomNumberGenerator;
        }

        public int NewBit()
        {
            return _rng.NextDouble() <= 0.5 ? 0 : 1;
        }


        private static int RandomBit()
        {
            return new Random().NextDouble() < 0.5 ? 1 : 0;
        }
    }
}
using System;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class CrossOverDecider
    {
        private readonly Random _rng;
        private readonly double _probabilityOfCrossover;

        public CrossOverDecider(Random rng, double probabilityOfCrossover)
        {
            _rng = rng;
            _probabilityOfCrossover = probabilityOfCrossover;
        }

        public bool ShouldCrossOver()
        {
            return _rng.NextDouble() <= _probabilityOfCrossover;
        }
    }
}
using System;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class RandomCrossOverIndexPicker : CrossOverIndexPicker
    {
        private readonly Random _rng;

        public RandomCrossOverIndexPicker(Random rng)
        {
            _rng = rng;
        }

        public int IndexToCrossOver(int chromosoneLength)
        {
            return (int) (chromosoneLength*_rng.NextDouble());
        }
    }
}
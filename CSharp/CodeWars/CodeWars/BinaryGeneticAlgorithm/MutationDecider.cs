using System;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class MutationDecider
    {
        private readonly Random _rng;
        private readonly double _probabilityOfMutation;

        public MutationDecider(Random rng, double probabilityOfMutation)
        {
            _rng = rng;
            _probabilityOfMutation = probabilityOfMutation;
        }

        public bool ShouldMutate()
        {
            return _rng.NextDouble() <= _probabilityOfMutation;
        }
    }
}
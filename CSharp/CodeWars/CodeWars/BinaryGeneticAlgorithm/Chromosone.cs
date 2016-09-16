using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class Chromosone
    {
        private readonly IList<int> _bits;

        public Chromosone(IList<int> bits)
        {
            _bits = bits;
        }

        public int Length()
        {
            return _bits.Count;
        }

        public override string ToString()
        {
            return string.Join(string.Empty, _bits);
        }

        public Chromosone Mutated(MutationDecider decider)
        {
            var rng = new Random();
            var newBits = 
                _bits
                    .Select(b => decider.ShouldMutate() ? Mutate(b) : b)
                    .ToList();
            return new Chromosone(newBits);
        }

        private int Mutate(int bit)
        {
            return bit == 1 ? 0 : 1;
        }

        public IList<Chromosone> SplitAt(int bitToSplitAt)
        {
            return new[]
            {
                new Chromosone(_bits.Take(bitToSplitAt).ToList()),
                new Chromosone(_bits.Skip(bitToSplitAt).ToList())
            };
        }

        public Chromosone Append(Chromosone other)
        {
            var newBits = _bits.ToList();
            newBits.AddRange(other._bits.ToList());
            return new Chromosone(newBits);
        }
    }
}
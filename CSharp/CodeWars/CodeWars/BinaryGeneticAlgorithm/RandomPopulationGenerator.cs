using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class RandomPopulationGenerator : PopulationGenerator
    {
        private readonly BitGenerator _generator;

        public RandomPopulationGenerator(BitGenerator generator)
        {
            _generator = generator;
        }

        public IList<Chromosone> CreatePopulationOf(int numberOfChromosones, int lengthOfChromosones)
        {
            return Enumerable.Range(1, numberOfChromosones)
                .Select(_ => new Chromosone(BitSequenceOfLength(lengthOfChromosones)))
                .ToList();
        }

        private IList<int> BitSequenceOfLength(int length)
        {
            return 
                Enumerable.Range(1, length)
                    .Select(_ => _generator.NewBit())
                    .ToList();
        }
    }
}
using System.Collections.Generic;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public interface PopulationGenerator
    {
        IList<Chromosone> CreatePopulationOf(int numberOfChromosones, int lengthOfChromosones);
    }
}
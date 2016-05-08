using System.Collections.Generic;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public interface ParentSelector
    {
        Parents SelectParents(RatedPopulation currentRatedPopulation);
        Parents SelectParents(IList<ChromosoneWithFitness> currentRatedPopulation);
    }
}
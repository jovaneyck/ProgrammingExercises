using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class RatedPopulation
    {
        private readonly IList<ChromosoneWithFitness> _ratedChromosones;

        public RatedPopulation(IEnumerable<ChromosoneWithFitness> ratedChromosones)
        {
            _ratedChromosones = ratedChromosones.OrderByDescending(rc => rc.Rating).ToList();
        }

        public int Size => _ratedChromosones.Count;

        public bool ContainsPerfectChromosone()
        {
            return _ratedChromosones.Any(c => c.Rating == 1.0d);
        }

        public ChromosoneWithFitness Best()
        {
            return _ratedChromosones.OrderByDescending(c=>c.Rating).First();
        }

        public Parents ParentsSelectedBy(ParentSelector parentSelector)
        {
            return parentSelector.SelectParents(_ratedChromosones);
        }
    }
}
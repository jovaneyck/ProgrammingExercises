using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class RouletteWheelParentSelector : ParentSelector
    {
        public Parents SelectParents(RatedPopulation currentRatedPopulation)
        {
            return currentRatedPopulation.ParentsSelectedBy(this);
        }

        public Parents SelectParents(IList<ChromosoneWithFitness> population)
        {
            var totalFitness =
                population.Sum(chromosone => chromosone.Rating);

            var chromosonesWithProbabilities =
                population.Select(
                    chromosoneWithFitness =>
                        new
                        {
                            Probability = chromosoneWithFitness.Rating/totalFitness,
                            Chromosone = chromosoneWithFitness.Chromosone
                        });

            var chromosonesWithCummulativeProbabilitiesOfBeingSelected = new List<Tuple<double, Chromosone>>();
            var cummulativeProbability = 0d;
            foreach (var chromWithProp in chromosonesWithProbabilities)
            {
                cummulativeProbability += chromWithProp.Probability;
                chromosonesWithCummulativeProbabilitiesOfBeingSelected.Add(
                    new Tuple<double, Chromosone>(cummulativeProbability, chromWithProp.Chromosone));
            }
            
            var rng = new Random();
            var fatherCummulativeProbability = rng.NextDouble()*cummulativeProbability;
            var father =
                chromosonesWithCummulativeProbabilitiesOfBeingSelected
                    .First(c => c.Item1 >= fatherCummulativeProbability)
                    .Item2;
            var motherCummulativeProbability = rng.NextDouble()*cummulativeProbability;
            var mother =
                chromosonesWithCummulativeProbabilitiesOfBeingSelected
                    .First(c => c.Item1 >= motherCummulativeProbability)
                    .Item2;

            return new Parents(father, mother);
        }
    }
}
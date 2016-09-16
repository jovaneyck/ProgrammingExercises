using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars.BinaryGeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        public int POPULATION_SIZE => 1000;

        private readonly Random _rng;
        private readonly PopulationGenerator _populationGenerator;
        private readonly ParentSelector _parentSelector;
        private MutationDecider _mutationDecider;
        private readonly CrossOverIndexPicker _crossOverIndexPicker;
        private CrossOverDecider _crossOverDecider;

        public GeneticAlgorithm(PopulationGenerator populationGenerator, ParentSelector parentSelector)
        {
            _rng = new Random();
            _populationGenerator = populationGenerator ?? new RandomPopulationGenerator(new RandomBitGenerator(_rng));
            _parentSelector = parentSelector ?? new RouletteWheelParentSelector();
            _crossOverIndexPicker = new RandomCrossOverIndexPicker(_rng);
            
        }

        public GeneticAlgorithm()
            : this(null, null)
        {
        }

        public string Run(
            Func<string, double> fitnessEvaluator, 
            int chromosomeLength, 
            double probabilityOfCrossover, 
            double probabilityOfMutation, 
            int numberOfIterations = 100)
        {
            //Ideally belongs in composition root, but need the config passed in through the public API
            _mutationDecider = new MutationDecider(_rng, probabilityOfMutation);
            _crossOverDecider = new CrossOverDecider(_rng, probabilityOfCrossover);

            var currentIteration = 1;
            var currentRatedPopulation = 
                Rate(
                    _populationGenerator.CreatePopulationOf(
                        POPULATION_SIZE, 
                        chromosomeLength), 
                    fitnessEvaluator);

            while (
                currentIteration <= numberOfIterations 
                && ! currentRatedPopulation.ContainsPerfectChromosone())
            {
                currentRatedPopulation = 
                    Rate(
                        NextPopulation(currentRatedPopulation),
                        fitnessEvaluator);

                currentIteration++;
            }

            return currentRatedPopulation.Best().Chromosone.ToString();
        }

        private IList<Chromosone> NextPopulation(RatedPopulation currentRatedPopulation)
        {
            var nextPopulation = new List<Chromosone>();
            while (nextPopulation.Count < currentRatedPopulation.Size)
            {
                var parents = _parentSelector.SelectParents(currentRatedPopulation);

                nextPopulation.Add(parents.Father);
                nextPopulation.Add(parents.Mother);

                if (_crossOverDecider.ShouldCrossOver())
                {
                    var crossedOver = parents.CrossOver(_crossOverIndexPicker);

                    nextPopulation.Add(crossedOver.Father.Mutated(_mutationDecider));
                    nextPopulation.Add(crossedOver.Mother.Mutated(_mutationDecider));
                }
            }
            return nextPopulation;
        }

        private static RatedPopulation Rate(IList<Chromosone> startingPopulation, Func<string, double> fitnessEvaluator)
        {
            return new RatedPopulation(
                startingPopulation
                    .Select(chromosone => new ChromosoneWithFitness(chromosone, fitnessEvaluator(chromosone.ToString()))));
        }
    }
}
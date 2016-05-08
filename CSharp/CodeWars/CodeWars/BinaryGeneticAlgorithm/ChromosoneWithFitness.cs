namespace CodeWars.BinaryGeneticAlgorithm
{
    public class ChromosoneWithFitness
    {
        public double Rating { get; }
        public Chromosone Chromosone { get; }

        public ChromosoneWithFitness(Chromosone chromosone, double rating)
        {
            Rating = rating;
            Chromosone = chromosone;
        }
    }
}
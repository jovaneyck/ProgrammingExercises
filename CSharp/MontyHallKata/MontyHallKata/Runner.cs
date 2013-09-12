namespace MontyHallKata
{
    public class Runner : SimulationRunner
    {
        private readonly SimulationParameterFactory parameterFactory;

        public Runner(SimulationParameterFactory parameterFactory)
        {
            this.parameterFactory = parameterFactory;
        }

        public void RunSimulations(SimulationResultListener listener, int numberOfSimulations, bool switchesDoors)
        {
            int numberOfSuccesses = 0;
            for (int simulationNumber = 1; simulationNumber <= numberOfSimulations; simulationNumber++ )
            {
                if (parameterFactory.GenerateParameters(switchesDoors).ResultsInAWin())
                    numberOfSuccesses++;
            }

            int numberOfLosses = numberOfSimulations - numberOfSuccesses;

            listener.ReceiveSimulationResults(numberOfSuccesses, numberOfLosses);
        }
    }
}
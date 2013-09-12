namespace MontyHallKata
{
    public class Runner : SimulationRunner
    {
        private readonly ResultChecker checker;
        private readonly SimulationParameterFactory parameterFactory;

        public Runner(ResultChecker checker, SimulationParameterFactory parameterFactory)
        {
            this.checker = checker;
            this.parameterFactory = parameterFactory;
        }

        public void RunSimulations(SimulationResultListener listener, int numberOfSimulations, bool switchesDoors)
        {
            int numberOfSuccesses = 0;
            for (int simulationNumber = 1; simulationNumber <= numberOfSimulations; simulationNumber++ )
            {
                if (checker.WinsTheGameWhen(parameterFactory.GenerateParameters(switchesDoors)))
                    numberOfSuccesses++;
            }

            int numberOfLosses = numberOfSimulations - numberOfSuccesses;

            listener.ReceiveSimulationResults(numberOfSuccesses, numberOfLosses);
        }
    }
}
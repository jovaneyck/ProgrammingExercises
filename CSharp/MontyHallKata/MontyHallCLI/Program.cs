using System;
using MontyHallKata;

namespace MontyHallCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfSimulations = 1000000;

            SimulationRunner runner = new Runner(new ConcreteResultChecker(), new RandomSimulationParametersFactory(new RandomDoorNumberGenerator()));
            
            runner.RunSimulations(new PrintToConsoleListener(true), numberOfSimulations, true);
            runner.RunSimulations(new PrintToConsoleListener(false), numberOfSimulations, false);

            Console.ReadKey();
        }
    }

    internal class PrintToConsoleListener : SimulationResultListener
    {
        private readonly bool _switchesDoors;

        public PrintToConsoleListener(bool switchesDoors)
        {
            _switchesDoors = switchesDoors;
        }

        public void ReceiveSimulationResults(int numberOfSuccesses, int numberOfFailures)
        {
            double percentage = (double) numberOfSuccesses / (numberOfSuccesses + numberOfFailures) * 100;

            Console.WriteLine("Won in {0}% of cases when {1}switching doors!", 
                percentage,
                _switchesDoors ? "" : "not "
                );
        }
    }
}

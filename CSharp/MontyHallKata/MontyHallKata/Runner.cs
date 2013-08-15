namespace MontyHallKata
{
    public class Runner : SimulationRunner
    {
        public void RunSimulations(SimulationResultListener listener, int numberOfSimulations)
        {
            listener.ReceiveSimulationResults(numberOfSimulations);
        }
    }
}
namespace MontyHallKata
{
    public interface SimulationResultListener
    {
        void ReceiveSimulationResults(int nbSuccesses, int nbFailures);
    }
}
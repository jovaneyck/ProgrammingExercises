namespace MontyHallKata
{
    public interface SimulationResultListener
    {
        void ReceiveSimulationResults(int numberOfSuccesses, int numberOfFailures);
    }
}
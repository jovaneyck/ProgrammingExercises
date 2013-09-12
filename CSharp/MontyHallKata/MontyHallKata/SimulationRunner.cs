namespace MontyHallKata
{
    public interface SimulationRunner
    {
        void RunSimulations(SimulationResultListener listener, int numberOfSimulations, bool switchesDoors);
    }
}
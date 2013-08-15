using System;

namespace MontyHallKata
{
    public class Runner : SimulationRunner
    {
        public void RunSimulations(SimulationResultListener listener)
        {
            listener.ReceiveSimulationResults(1);
        }
    }
}
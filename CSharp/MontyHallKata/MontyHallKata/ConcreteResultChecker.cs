namespace MontyHallKata
{
    public class ConcreteResultChecker : ResultChecker
    {
        public bool WinsTheGameWhen(SimulationParameters parameters)
        {
            return parameters.ResultsInAWin();
        }
    }
}
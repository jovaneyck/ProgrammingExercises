namespace MontyHallKata
{
    public class ConcreteResultChecker : ResultChecker
    {
        public bool WinsTheGameWhen(SimulationParameters parameters)
        {
            return (FirstChosenDoorIsTheCorrectOneAndDoesNotSwitch(parameters) || FirstChosenDoorIsTheWrongOneAndDoesSwitches(parameters));
        }

        private bool FirstChosenDoorIsTheCorrectOneAndDoesNotSwitch(SimulationParameters parameters)
        {
            return parameters.InitiallyChosenDoor == parameters.CorrectDoor && ! parameters.SwitchesDoors;
        }

        private bool FirstChosenDoorIsTheWrongOneAndDoesSwitches(SimulationParameters parameters)
        {
            return parameters.InitiallyChosenDoor != parameters.CorrectDoor && parameters.SwitchesDoors;
        }
    }
}
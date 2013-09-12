namespace MontyHallKata
{
    public class SimulationParameters
    {
        public bool SwitchesDoors { get; set; }
        public int CorrectDoor { get; set; }
        public int InitiallyChosenDoor { get; set; }

        public bool ResultsInAWin()
        {
            return (FirstChosenDoorIsTheCorrectOneAndDoesNotSwitch() || FirstChosenDoorIsTheWrongOneAndDoesSwitches());
        }

        private bool FirstChosenDoorIsTheCorrectOneAndDoesNotSwitch()
        {
            return InitiallyChosenDoor == CorrectDoor && !SwitchesDoors;
        }

        private bool FirstChosenDoorIsTheWrongOneAndDoesSwitches()
        {
            return InitiallyChosenDoor != CorrectDoor && SwitchesDoors;
        }
    }
}
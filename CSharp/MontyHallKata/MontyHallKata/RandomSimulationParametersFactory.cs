namespace MontyHallKata
{
    public class RandomSimulationParametersFactory : SimulationParameterFactory
    {
        private readonly DoorNumberGenerator generator;

        public RandomSimulationParametersFactory(DoorNumberGenerator numberGenerator)
        {
            generator = numberGenerator;
        }

        public SimulationInstance GenerateParameters(bool switchesDoors)
        {
            return new SimulationInstance()
                       {
                           CorrectDoor = generator.RoomNumber(),
                           InitiallyChosenDoor = generator.RoomNumber(),
                           SwitchesDoors = switchesDoors
                       };
        }
    }
}
namespace MontyHallKata
{
    public class RandomSimulationParametersFactory : SimulationParameterFactory
    {
        private readonly DoorNumberGenerator generator;

        public RandomSimulationParametersFactory(DoorNumberGenerator numberGenerator)
        {
            generator = numberGenerator;
        }

        public SimulationParameters GenerateParameters(bool switchesDoors)
        {
            return new SimulationParameters()
                       {
                           CorrectDoor = generator.RoomNumber(),
                           InitiallyChosenDoor = generator.RoomNumber(),
                           SwitchesDoors = switchesDoors
                       };
        }
    }
}
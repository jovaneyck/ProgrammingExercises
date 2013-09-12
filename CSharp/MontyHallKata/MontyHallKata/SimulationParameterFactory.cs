namespace MontyHallKata
{
    public interface SimulationParameterFactory
    {
        SimulationInstance GenerateParameters(bool switchesDoors);
    }
}
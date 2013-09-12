namespace MontyHallKata
{
    public interface SimulationParameterFactory
    {
        SimulationParameters GenerateParameters(bool switchesDoors);
    }
}
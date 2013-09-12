using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class RunnerTest
    {
        private MockFactory context;

        [SetUp]
        public void SetUp()
        {
            context = new MockFactory();
        }

        [TearDown]
        public void TearDown()
        {
            context.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void NotifiesListenersOfResults()
        {
            var listener = context.CreateMock<SimulationResultListener>();
            listener.Expects.AtLeastOne.Method(l => l.ReceiveSimulationResults(0,0)).WithAnyArguments();

            var simulationParameterFactory = context.CreateInstance<SimulationParameterFactory>();
            Runner runner = new Runner(simulationParameterFactory);
            runner.RunSimulations(listener.MockObject, 0, false);
        }

        [Test]
        public void StartsTheCorrectNumberOfSimulations()
        {
            var parameterFactory = context.CreateMock<SimulationParameterFactory>();
            var simulationInstance = context.CreateInstance<SimulationInstance>();
            parameterFactory.Expects.Exactly(5).Method(f => f.GenerateParameters(false)).WithAnyArguments().WillReturn(simulationInstance);

            Runner runner = new Runner(parameterFactory.MockObject);
            var listener = context.CreateInstance<SimulationResultListener>(MockStyle.Stub);
            
            runner.RunSimulations(listener, 5, false);
        }
    }
}
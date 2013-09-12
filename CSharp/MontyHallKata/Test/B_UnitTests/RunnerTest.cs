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

            var simulationFactory = context.CreateInstance<ResultChecker>();
            var simulationParameterFactory = context.CreateInstance<SimulationParameterFactory>();
            Runner runner = new Runner(simulationFactory, simulationParameterFactory);
            runner.RunSimulations(listener.MockObject, 0, false);
        }

        [Test]
        public void StartsTheCorrectNumberOfSimulations()
        {
            var factory = context.CreateMock<ResultChecker>();
            factory.Expects.Exactly(5).Method(f => f.WinsTheGameWhen(null)).WithAnyArguments().WillReturn(false);
            var parameterFactory = context.CreateInstance<SimulationParameterFactory>(MockStyle.Stub);

            Runner runner = new Runner(factory.MockObject, parameterFactory);
            var listener = context.CreateInstance<SimulationResultListener>(MockStyle.Stub);
            
            runner.RunSimulations(listener, 5, false);
        }
    }
}
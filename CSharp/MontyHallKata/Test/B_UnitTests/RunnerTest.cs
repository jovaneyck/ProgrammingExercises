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
            Mock<SimulationResultListener> listener = context.CreateMock<SimulationResultListener>();
            listener.Expects.AtLeastOne.Method(l => l.ReceiveSimulationResults(0)).WithAnyArguments();

            var runner = new Runner();
            runner.RunSimulations(listener.MockObject, 0);
        }
    }
}
using System;
using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class MainWindowTest
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
        [STAThread]
        public void CanStartASimulation()
        {
            Mock<SimulationRunner> runner = context.CreateMock<SimulationRunner>();
            runner.Expects.AtLeastOne.Method(r => r.RunSimulations(null, 0)).WithAnyArguments();

            var window = new MainWindow(runner.MockObject);
            window.startSimulations_Click(null, null);
        }
    }
}
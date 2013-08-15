using System;
using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class MainWindowTest
    {
        #region Setup/Teardown

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

        #endregion

        private MockFactory context;

        [Test]
        [STAThread]
        public void CanStartASimulation()
        {
            Mock<SimulationRunner> runner = context.CreateMock<SimulationRunner>();
            runner.Expects.AtLeastOne.Method(r => r.RunSimulations(null)).WithAnyArguments();

            var window = new MainWindow(runner.MockObject);
            window.startSimulations_Click(null, null);
        }
    }
}
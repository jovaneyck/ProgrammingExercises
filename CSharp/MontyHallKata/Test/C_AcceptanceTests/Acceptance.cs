using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace Test.C_AcceptanceTests
{
    [TestFixture]
    public class Acceptance
    {
        private Application application;
        private Window mainWindow;

        [SetUp]
        public void StartApplication()
        {
            var startInfo = new ProcessStartInfo("MontyHallKata.exe");
            application = Application.Launch(startInfo);
            mainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);
        }

        [TearDown]
        public void TearDown()
        {
            mainWindow.Close();
            application.Close();
        }

        [Test]
        public void RendersResultsOfASingleSimulation()
        {
            StartSimulations(1);
            Assert.AreEqual(1, GetNumberOfSuccesses() + GetNumberOfFailures());
        }

        private void StartSimulations(int numberOfSimulations)
        {
            SetNumberOfExperiments(numberOfSimulations);
            RunSimulations();
        }

        private void SetNumberOfExperiments(int numberOfExperiments)
        {
            var numberOfExperimentsInput = mainWindow.Get<TextBox>("numberOfSimulations");
            numberOfExperimentsInput.Text = numberOfExperiments.ToString(CultureInfo.InvariantCulture);
        }

        private void RunSimulations()
        {
            var startSimulationButton = mainWindow.Get<Button>("startSimulation");
            startSimulationButton.Click();
        }

        private int GetNumberOfSuccesses()
        {
            return ValueOf("numberOfSuccesses");
        }

        private int ValueOf(string labelName)
        {
            var label = mainWindow.Get<Label>(labelName);
            if (label.Text.Equals(string.Empty))
                return 0;
            return int.Parse(label.Text);
        }

        private int GetNumberOfFailures()
        {
            return ValueOf("numberOfFailures");
        }

        [Test]
        public void RendersResultsOfMultipleSimulations()
        {
            const int totalNumberOfSimulations = 100;

            StartSimulations(totalNumberOfSimulations);

            var numberOfSuccesses = GetNumberOfSuccesses();
            var numberOfFailures = GetNumberOfFailures();

            Assert.IsTrue(numberOfSuccesses > 0, "At least one succesful simulation");
            Assert.IsTrue(numberOfFailures > 0, "At least one unsuccessful simulation");
            Assert.AreEqual(totalNumberOfSimulations + numberOfFailures, numberOfSuccesses);
        }
    }
}
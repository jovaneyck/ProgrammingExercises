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

        [TearDown]
        public void TearDown()
        {
            mainWindow.Close();
            application.Close();
        }

        [Test]
        public void RendersResultsOfASingleSimulation()
        {
            StartApplication();
            StartASingleSimulation();
            AssertSingleExperimentHasASuccessfullOutcome();
        }

        public void StartApplication()
        {
            var startInfo = new ProcessStartInfo("MontyHallKata.exe");
            application = Application.Launch(startInfo);
            mainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);
        }

        private void StartASingleSimulation()
        {
            SetNumberOfExperiments(1);
            RunSimulations();
        }

        private void SetNumberOfExperiments(int numberOfExperiments)
        {
            var numberOfExperimentsInput = mainWindow.Get<TextBox>("numberOfExperiments");
            numberOfExperimentsInput.Text = numberOfExperiments.ToString(CultureInfo.InvariantCulture);
        }

        private void RunSimulations()
        {
            var startSimulationButton = mainWindow.Get<Button>("startSimulation");
            startSimulationButton.Click();
        }

        private void AssertSingleExperimentHasASuccessfullOutcome()
        {
            AssertLabelContainsValue("numberOfSuccesses", "1");
        }

        private void AssertLabelContainsValue(string labelName, string value)
        {
            var label = mainWindow.Get<Label>(labelName);
            Assert.AreEqual(value, label.Text);
        }
    }
}
using System.Diagnostics;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace Test
{
    [TestFixture]
    public class Acceptance
    {
        private Application application;
        private Window mainWindow;

        [SetUp]
        public void SetUp()
        {
            //As there is no straightforward way of replacing the dependency of a random number generator using plain White, we are working with a fixed seed.
            //Note that this can be achieved by spawning another thread which programmatically starts the application, this is somehting I should look into before starting the real implementation!
            const string commandLineArguments = "seed:1337";
            ProcessStartInfo startInfo = new ProcessStartInfo("MontyHallKata.exe", commandLineArguments);
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
           StartASingleSimulation();
            AssertSingleExperimentHasASuccessfullOutcome();
        }

        private void StartASingleSimulation()
        {
            TextBox numberOfExperimentsInput = mainWindow.Get<TextBox>("numberOfExperiments");
            numberOfExperimentsInput.Text = "1";

            Button startSimulationButton = mainWindow.Get<Button>("startSimulation");
            startSimulationButton.Click();
        }

        private void AssertSingleExperimentHasASuccessfullOutcome()
        {
            AssertLabelContainsValue("numberOfSuccesses", "1");
        }

        private void AssertLabelContainsValue(string labelName, string value)
        {
            Label label = mainWindow.Get<Label>(labelName);
            Assert.AreEqual(value, label.Text);
        }
    }
}

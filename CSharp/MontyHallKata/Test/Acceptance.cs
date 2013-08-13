using System;
using System.Collections.Generic;
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
        private const int NumberOfExperiments = 10000;

        [TearDown]
        public void TearDown()
        {
            mainWindow.Close();
            application.Close();
        }

        [Test]
        public void RendersResultsOfASingleSimulation()
        {
            StartApplicationWithRandomNumbers(GenerateRandomNumbers(0));
            StartASingleSimulation();
            AssertSingleExperimentHasASuccessfullOutcome();
        }

        private List<int> GenerateRandomNumbers(int value)
        {
            List<int> randomNumbers = new List<int>();

            for (int i = 0; i < NumberOfExperiments; i++)
            {
                randomNumbers.Add(value);
            }

            return randomNumbers;
        }

        public void StartApplicationWithRandomNumbers(List<int> randomNumbers)
        {
            //Pass in the random numbers as a command-line argument. This is NOT something I would ever do in a real application :(
            var randomNumbersAsString = RandomNumbersAsString(randomNumbers);

            string commandLineArguments = String.Format("rng:{0}", randomNumbersAsString);
            ProcessStartInfo startInfo = new ProcessStartInfo("MontyHallKata.exe", commandLineArguments);
            application = Application.Launch(startInfo);
            mainWindow = application.GetWindow("MainWindow", InitializeOption.NoCache);
        }

        private string RandomNumbersAsString(IEnumerable<int> randomNumbers)
        {
            string randomNumbersAsString = "";

            foreach (int number in randomNumbers)
            {
                if (!randomNumbersAsString.Equals(String.Empty))
                    randomNumbersAsString += ", ";

                randomNumbersAsString += number;
            }
            return randomNumbersAsString;
        }

        private void StartASingleSimulation()
        {
            SetNumberOfExperiments(1);
            RunSimulations();
        }

        private void SetNumberOfExperiments(int numberOfExperiments)
        {
            TextBox numberOfExperimentsInput = mainWindow.Get<TextBox>("numberOfExperiments");
            numberOfExperimentsInput.Text = numberOfExperiments.ToString();
        }

        private void RunSimulations()
        {
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

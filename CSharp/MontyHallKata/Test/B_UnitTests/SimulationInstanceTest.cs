using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class SimulationInstanceTest
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
        public void WinsTheGameWhenHePicksTheCorrectDoorAndDoesNotSwitch()
        {
            const int correctDoor = 1;
            const int chosenDoor = correctDoor;
            const bool switchesDoors = false;
            Assert.IsTrue(WinsWhen(switchesDoors, correctDoor, chosenDoor));
        }

        private bool WinsWhen(bool switches, int correctDoor, int chosenDoor)
        {
            return (new SimulationInstance() { SwitchesDoors = switches, CorrectDoor = correctDoor, InitiallyChosenDoor = chosenDoor }).ResultsInAWin();
        }

        [Test]
        public void LosesTheGameWhenHePicksTheCorrectDoorAndSwitches()
        {
            const int correctDoor = 1;
            const int chosenDoor = correctDoor;
            const bool switchesDoors = true;
            Assert.IsFalse(WinsWhen(switchesDoors, correctDoor, chosenDoor));
        }

        [Test]
        public void LosesTheGameWhenHePicksTheWrongDoorAndDoesNotSwitch()
        {
            const int correctDoor = 1;
            const int chosenDoor = 2;
            const bool switchesDoors = false;
            Assert.IsFalse(WinsWhen(switchesDoors, correctDoor, chosenDoor));
        }

        [Test]
        public void WinsTheGameWhenHePicksTheWrongDoorAndSwitches()
        {
            const int correctDoor = 1;
            const int chosenDoor = 2;
            const bool switchesDoors = true;
            Assert.IsTrue(WinsWhen(switchesDoors, correctDoor, chosenDoor));
        }
    }
}
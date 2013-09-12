using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class ResultCheckerTest
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

            ConcreteResultChecker checker = new ConcreteResultChecker();
            bool hasWonTheGame = checker.WinsTheGameWhen(new SimulationParameters() { SwitchesDoors = false, CorrectDoor = correctDoor, InitiallyChosenDoor = correctDoor });

            Assert.IsTrue(hasWonTheGame);
        }

        [Test]
        public void LosesTheGameWhenHePicksTheCorrectDoorAndSwitches()
        {
            const int correctDoor = 1;

            ConcreteResultChecker simulation = new ConcreteResultChecker();
            bool hasWonTheGame = simulation.WinsTheGameWhen(new SimulationParameters() { SwitchesDoors = true, CorrectDoor = correctDoor, InitiallyChosenDoor = correctDoor });

            Assert.IsFalse(hasWonTheGame);
        }

        [Test]
        public void LosesTheGameWhenHePicksTheWrongDoorAndDoesNotSwitch()
        {
            ConcreteResultChecker simulation = new ConcreteResultChecker();
            bool hasWonTheGame = simulation.WinsTheGameWhen(new SimulationParameters() { SwitchesDoors = false, CorrectDoor = 1, InitiallyChosenDoor = 2 });

            Assert.IsFalse(hasWonTheGame);
        }        

        [Test]
        public void LosesTheGameWhenHePicksTheWrongDoorAndTheFirstOpenDoorEndsUpBeingTheCorrectDoor()
        {
            ConcreteResultChecker simulation = new ConcreteResultChecker();
            bool hasWonTheGame = simulation.WinsTheGameWhen(new SimulationParameters() { SwitchesDoors = false, CorrectDoor = 1, InitiallyChosenDoor = 2 });

            Assert.IsFalse(hasWonTheGame);
        }

        [Test]
        public void WinsTheGameWhenHePicksTheWrongDoorAndSwitches()
        {
            ConcreteResultChecker simulation = new ConcreteResultChecker();
            bool hasWonTheGame = simulation.WinsTheGameWhen(new SimulationParameters() { SwitchesDoors = true, CorrectDoor = 1, InitiallyChosenDoor = 2 });

            Assert.IsTrue(hasWonTheGame);
        }
    }
}
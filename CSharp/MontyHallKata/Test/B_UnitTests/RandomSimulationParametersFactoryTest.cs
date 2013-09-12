using MontyHallKata;
using NMock;
using NUnit.Framework;

namespace Test.B_UnitTests
{
    [TestFixture]
    internal class RandomSimulationParametersFactoryTest
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
        public void GeneratesSimulationParameters()
        {
            Mock<DoorNumberGenerator> numberGenerator = context.CreateMock<DoorNumberGenerator>();
            numberGenerator.Expects.AtLeastOne.Method(g => g.RoomNumber()).WillReturn(1);

            RandomSimulationParametersFactory factory = new RandomSimulationParametersFactory(numberGenerator.MockObject);

            const bool switchesDoors = true;
            SimulationInstance instance = factory.GenerateParameters(switchesDoors);

            Assert.AreEqual(instance.SwitchesDoors, switchesDoors);
            Assert.AreEqual(instance.InitiallyChosenDoor, 1);
            Assert.AreEqual(instance.CorrectDoor, 1);
        }
    }
}
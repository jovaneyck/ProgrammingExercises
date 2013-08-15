using NMock;
using NUnit.Framework;

namespace Test.A_Templates
{
    [TestFixture]
    internal class Template
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
        public void TestName()
        {
            Assert.IsTrue(true);
        }
    }
}
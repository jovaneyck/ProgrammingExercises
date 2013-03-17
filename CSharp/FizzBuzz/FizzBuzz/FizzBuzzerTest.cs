using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    class FizzBuzzerTest
    {
        [Test]
        public void HandlesOneCorrectly()
        {
            TestNormalNumber(1);
        }

        private void TestNormalNumber(int number)
        {
            Assert.AreEqual(number, FizzBuzzify(number));
        }

        [Test]
        public void HandlesTwoCorrectly()
        {
            TestNormalNumber(2);
        }

        private int FizzBuzzify(int number)
        {
            return number;
        }
    }
}

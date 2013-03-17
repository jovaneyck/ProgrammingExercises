using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    class FizzBuzzerTest
    {
        [Test]
        public void TransformsOneCorrectly()
        {
            const int number = 1;
            Assert.AreEqual(number, FizzBuzzify(number));
        }

        private int FizzBuzzify(int number)
        {
            return 1;
        }
    }
}

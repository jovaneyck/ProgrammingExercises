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

        [Test]
        public void TransformsTwoCorrectly()
        {
            const int number = 2;
            Assert.AreEqual(number, FizzBuzzify(number));
        }

        private int FizzBuzzify(int number)
        {
            return number;
        }
    }
}

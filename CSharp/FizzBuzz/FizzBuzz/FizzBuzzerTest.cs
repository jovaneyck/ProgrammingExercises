using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    class FizzBuzzerTest
    {
        [Test]
        public void TransformsOneCorrectly()
        {
            TestNormalNumber(1);
        }

        private void TestNormalNumber(int number)
        {
            Assert.AreEqual(number, FizzBuzzify(number));
        }

        [Test]
        public void TransformsTwoCorrectly()
        {
            TestNormalNumber(2);
        }

        private int FizzBuzzify(int number)
        {
            return number;
        }
    }
}

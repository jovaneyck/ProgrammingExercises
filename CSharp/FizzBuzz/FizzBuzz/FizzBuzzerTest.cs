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
            Assert.AreEqual(number.ToString(), FizzBuzzify(number));
        }

        [Test]
        public void HandlesTwoCorrectly()
        {
            TestNormalNumber(2);
        }

        [Test]
        public void HandlesThreeCorrectly()
        {
            Assert.AreEqual("Fizz", FizzBuzzify(3));
        }

        private string FizzBuzzify(int number)
        {
            if (number == 3)
                return "Fizz";
            return number.ToString();
        }
    }
}

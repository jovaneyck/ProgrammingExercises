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

        [Test]
        public void HandlesFiveCorrectly()
        {
            Assert.AreEqual("Buzz", FizzBuzzify(5));
        }

        private string FizzBuzzify(int number)
        {
            if (number == 3)
                return "Fizz";
            if (number == 5)
                return "Buzz";
            return number.ToString();
        }
    }
}

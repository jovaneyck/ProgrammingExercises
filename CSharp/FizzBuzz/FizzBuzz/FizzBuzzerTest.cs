using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    class FizzBuzzerTest
    {
        [Test]
        public void HandlesOneCorrectly()
        {
            TestNumber("1", 1);
        }

        private void TestNumber(string expected, int number)
        {
            Assert.AreEqual(expected, FizzBuzzify(number));
        }

        [Test]
        public void HandlesTwoCorrectly()
        {
            TestNumber("2", 2);
        }

        [Test]
        public void HandlesThreeCorrectly()
        {
            TestNumber("Fizz", 3);
        }

        [Test]
        public void HandlesFiveCorrectly()
        {
            TestNumber("Buzz", 5);
        }

        [Test]
        public void Six()
        {
            TestNumber("Fizz", 6);
        }

        [Test]
        public void Ten()
        {
            TestNumber("Buzz", 10);
        }

        [Test]
        public void Fifteen()
        {
            TestNumber("FizzBuzz", 15);
        }

        [Test]
        public void Thirty()
        {
            TestNumber("FizzBuzz", 30);
        }

        private string FizzBuzzify(int number)
        {
            if (number % 15 == 0)
                return "FizzBuzz";
            if (number % 3 == 0)
                return "Fizz";
            if (number % 5 == 0)
                return "Buzz";
            return number.ToString();
        }
    }
}

using NUnit.Framework;

namespace FizzBuzz
{
    [TestFixture]
    class FizzBuzzerTest
    {
        private FizzBuzz _fizzBuzz;

        [SetUp]
        public void SetUp()
        {
            _fizzBuzz = new FizzBuzz();
        }

        [Test]
        public void HandlesOneCorrectly()
        {
            TestNumber("1", 1);
        }

        private void TestNumber(string expected, int number)
        {
            Assert.AreEqual(expected, _fizzBuzz.FizzBuzzify(number));
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
    }
}

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

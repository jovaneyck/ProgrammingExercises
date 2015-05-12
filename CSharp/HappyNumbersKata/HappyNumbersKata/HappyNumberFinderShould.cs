using Xunit;

namespace HappyNumbersKata
{
    public class HappyNumberFinderShould
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public void RecognizeHappyNumbers(int numberToCheck, bool isHappyNumber)
        {
            Assert.Equal(isHappyNumber, new HappyNumberFinder().IsHappy(numberToCheck));
        }         
    }

    internal class HappyNumberFinder
    {
        public bool IsHappy(int number)
        {
            return number == 1;
        }
    }
}
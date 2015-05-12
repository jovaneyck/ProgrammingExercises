using Xunit;

namespace HappyNumbersKata
{
    public class HappyNumberFinderShould
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(31, true)] //31 -> 10 -> 1
        [InlineData(4, false)] //4 -> 16 -> 37 -> 58 -> 89 -> 145 -> 42 -> 20 -> 4
        public void RecognizeHappyNumbers(int numberToCheck, bool isHappyNumber)
        {
            Assert.Equal(isHappyNumber, new HappyNumberFinder().IsHappy(numberToCheck));
        }         
    }
}
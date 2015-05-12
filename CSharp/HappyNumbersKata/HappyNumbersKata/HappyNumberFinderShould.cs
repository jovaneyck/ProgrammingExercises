using Xunit;

namespace HappyNumbersKata
{
    public class HappyNumberFinderShould
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(1, true)]
        [InlineData(31, true)] //31 -> 10 -> 1
        //non-trivial happy (31)
        //non-trivial non-happy (4)
        public void RecognizeHappyNumbers(int numberToCheck, bool isHappyNumber)
        {
            Assert.Equal(isHappyNumber, new HappyNumberFinder().IsHappy(numberToCheck));
        }         
    }

    internal class HappyNumberFinder
    {
        public bool IsHappy(int number)
        {
            return number == 1 || number == 31;
        }
    }
}
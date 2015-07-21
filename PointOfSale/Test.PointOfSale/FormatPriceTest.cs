using PointOfSale;
using Xunit;

namespace Test.PointOfSale
{
    public class FormatPriceTest
    {
        [Theory]
        [InlineData(0, "$0.00")]
        [InlineData(10, "$0.10")]
        [InlineData(100, "$1.00")]
        [InlineData(133750, "$1,337.50")]
        public void FormatsPriceCorrectly(int priceInCents, string expectedResult)
        {
            Assert.Equal(expectedResult, new Price(priceInCents).Formatted());
        }
    }
}
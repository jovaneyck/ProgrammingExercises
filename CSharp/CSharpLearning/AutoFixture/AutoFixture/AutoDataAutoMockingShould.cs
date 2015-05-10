using Xunit;

namespace AutoFixture
{
    public class AutoDataAutoMockingShould
    {
        [Theory]
        [InlineData(1, 2)]
        public void SupportParametrizedTestsXUnitStyle(int input, int expected)
        {
            Assert.Equal(input + 1, expected);
        }
    }
}
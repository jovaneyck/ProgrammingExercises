using Ploeh.AutoFixture.Xunit2;
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

        class Adder
        {
            public int Add(int a, int b)
            {
                return a + b;
            } 
        }

        [Theory]
        [AutoData]
        public void ProvideDataAutomatically(int first, int second) //autofixture provides randomly created instances of input arguments
        {
            Assert.Equal(first+second, new Adder().Add(first, second));
        }
    }
}
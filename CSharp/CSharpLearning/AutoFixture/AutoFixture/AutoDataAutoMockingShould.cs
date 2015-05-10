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

        public class Adder
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

        [Theory]
        [AutoData]
        public void ProvideTheSUT(Adder adder, int first, int second) //the SUT can also be automagically instantiated using default fixture creation.
        {
            Assert.Equal(first + second, adder.Add(first, second));
        }
    }
}
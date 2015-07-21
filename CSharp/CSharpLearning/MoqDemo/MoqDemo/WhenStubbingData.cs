using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenStubbingData
    {
        public interface Foo
        {
            int SomeNumber();
        }

        [Fact]
        public void CanReturnPreCannedData()
        {
            var mocked = new Mock<Foo>();
            mocked
                .Setup(f => f.SomeNumber())
                .Returns(42);

            Assert.Equal(42, mocked.Object.SomeNumber());
        }        
    }
}
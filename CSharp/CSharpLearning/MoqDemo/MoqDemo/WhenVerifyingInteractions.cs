using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenVerifyingInteractions
    {
        [Fact]
        public void CanVerifyWhetherMethodWasCalled()
        {
            var mocked = new Mock<Foo>();

            mocked.Object.NoArguments();

            mocked.Verify(f=>f.NoArguments());
        }

        [Fact]
        public void CanVerifyWhetherMethodWasCalledACertainAmountOfTimes()
        {
            var mocked = new Mock<Foo>();

            mocked.Object.NoArguments();

            mocked.Verify(f=>f.NoArguments(), Times.AtLeastOnce);
        }
        [Fact]
        public void CanVerifyWhetherAMethodWasNeverCalled() //just in case you ever want to test something "not" occuring
        {
            var mocked = new Mock<Foo>();

            mocked.Verify(f=>f.NoArguments(), Times.Never);
        }

        public interface Foo
        {
            void NoArguments();
        }

        [Fact]
        public void CanSetUpGOOSStyleExpectations()
        {
            var mocked = new Mock<Foo>();
            mocked.Setup(f => f.NoArguments());

            mocked.Object.NoArguments();

            mocked.VerifyAll();
        }
    }
}
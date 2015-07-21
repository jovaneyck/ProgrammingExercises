using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenYouNeedThatSliiightlyMoreIntelligentMock
    {
        public interface Foo
        {
            int Echo(int number);
        }

        [Fact]
        public void CanUseTheInputArgumentsOfAMockedMethod()
        {
            var mocked = new Mock<Foo>();
            mocked
                .Setup(f => f.Echo(It.IsAny<int>()))
                .Returns((int input) => input); //You can wire up some logic (if you really really want to)

            Assert.Equal(1337, mocked.Object.Echo(1337));
        }

        [Fact]
        public void CanUseLazyEvaluationOnCannedResponses()
        {
            var aNumber = 0;

            var mocked = new Mock<Foo>();
            mocked
                .Setup(f => f.Echo(1))
                .Returns(() => aNumber); //Again, you can do this if you reaaally reaaaaally want this.

            //later, aNumber changes
            aNumber = 1337;

            Assert.Equal(1337, mocked.Object.Echo(1));
            
        }
    }
}
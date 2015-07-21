using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenArgumentMatching
    {
        public interface Foo
        {
            void Command(int argument);
        }

        [Fact]
        public void YouCanUseAnyArgumentMatcherIfYouDontReallyCare()
        {
            var mock = new Mock<Foo>();
            var aFoo = mock.Object;
            aFoo.Command(1);
            aFoo.Command(2);

            mock.Verify(f=>f.Command(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void YouCanUseMatchesToDefineAPredicate()
        {
            var mock = new Mock<Foo>();
            
            mock.Object.Command(1337);

            mock.Verify(f => f.Command(It.Is((int nb) => nb == 1337))); //You can use any predicate you want
        }

        [Fact]
        public void CanCreateACustomMatcher()
        {
            var mock = new Mock<Foo>();

            mock.Object.Command(1337);

            mock.Verify(f => f.Command(IsASpecificNumber()));
        }

        private int IsASpecificNumber()
        {
            return Match.Create((int nb) => nb == 1337);
        }
    }
}
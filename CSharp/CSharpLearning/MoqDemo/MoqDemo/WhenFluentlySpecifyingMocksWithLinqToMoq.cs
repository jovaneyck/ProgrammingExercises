using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenFluentlySpecifyingMocksWithLinqToMoq
    {
        public interface Foo
        {
            int Query(int someArgument);
            int OtherQuery(int someArgument);
        }

        [Fact]
        public void CanCreateAStubWithPrecannedDataInASingleLine()
        {
            var aFoo = 
                Mock.Of<Foo>(f => 
                    f.Query(1) == 1337 && 
                    f.OtherQuery(1) == 666); //one-line creation and setup!

            Assert.Equal(1337, aFoo.Query(1)); // No .Object required!
            Assert.Equal(666, aFoo.OtherQuery(1));
        }
    }
}
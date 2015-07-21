using System;
using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenTestingExceptionHandling
    {
        public interface Foo
        {
            void Command();
        }

        [Fact]
        public void CanEasilySetupExceptions()
        {
            var mock = new Mock<Foo>();
            mock.Setup(f => f.Command()).Throws<ArgumentException>();

            Assert.Throws<ArgumentException>(() => mock.Object.Command());
        }
    }
}
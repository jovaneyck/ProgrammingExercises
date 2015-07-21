using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenStubbingProperties
    {
        public class ConcreteClass
        {
            public int OneLevelDeep { get; set; }

            public virtual OtherClass One { get; set; }
        }

        public class OtherClass
        {
            public virtual AnotherClass Two { get; set; }
        }

        public class AnotherClass
        {
            public int Three { get; set; }
        }

        [Fact]
        public void CanEasilyStubAProperty()
        {
            var foo = Mock.Of<ConcreteClass>(f => f.OneLevelDeep == 1);

            Assert.Equal(1, foo.OneLevelDeep);
        }

        [Fact]
        public void CanEasilySetupAStubbedHierarchy()
        {
            var foo = Mock.Of<ConcreteClass>(f => f.One.Two.Three == 3); 
            //one-line setup of a three-level deep hierarchy.
            //requires interfaces or virtuals

            Assert.Equal(3, foo.One.Two.Three);
            
        }
    }
}
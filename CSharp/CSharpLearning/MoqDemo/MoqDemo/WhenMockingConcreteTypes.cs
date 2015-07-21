using Moq;
using Xunit;

namespace MoqDemo
{
    public class WhenMockingConcreteTypes
    {
        public class ClassWithDefaultConstructor
        {
        }

        [Fact]
        public void CanCreateMockWhenTypeHasADefaultConstructor()
        {
            var mocked = new Mock<ClassWithDefaultConstructor>().Object;
        }

        public class ClassWithoutDefaultConstructor
        {
            public ClassWithoutDefaultConstructor(int someDependency)
            {
            }
        }

        [Fact]
        public void CanCreateMockWhenThereIsNoDefaultConstructorByExplicitlyProvidingArguments()
        {
            //var mocked = new Mock<ClassWithoutDefaultConstructor>().Object; //will not work: no default constructor
            //var mocked = new Mock<ClassWithoutDefaultConstructor>("a string").Object; //will not work: arguments do not match
            var mocked = new Mock<ClassWithoutDefaultConstructor>(33).Object;
        }

        [Fact]
        public void MethodsHaveToBeDeclaredVirtualInOrderToBeMocked()
        {
            var mocked = new Mock<Foo>();
            //mocked    //Will not work since the method is not declared virtual
            //    .Setup(f => f.NotVirtual())
            //    .Returns(1);

            mocked
                .Setup(f => f.Virtual())
                .Returns(33);

            Assert.Equal(33, mocked.Object.Virtual());
        }

        public class Foo
        {
            public int NotVirtual()
            {
                throw new System.NotImplementedException();
            }

            public virtual int Virtual()
            {
                throw new System.NotImplementedException();
            }
        }
    }

}

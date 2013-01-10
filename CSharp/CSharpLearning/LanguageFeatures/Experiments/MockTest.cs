using NUnit.Framework;
using Rhino.Mocks;

namespace LanguageFeatures.Experiments
{
    public interface IToMock
    {
        string HelloMessage();
    }

    public class ClassUnderTest
    {
        public string StoredMessage { get; set; }

        public ClassUnderTest(IToMock dependency)
        {
           StoredMessage = dependency.HelloMessage();
        }
    }

    [TestFixture]
    public class MockTest
    {
         [Test]
         public void BasicMockingTest()
         {
             var message = "Hello world!";
             var dependency = MockRepository.GenerateMock<IToMock>();
             dependency.Stub(x => x.HelloMessage()).Return(message);

             var classUnderTest = new ClassUnderTest(dependency);
             
             Assert.AreEqual(message, classUnderTest.StoredMessage);
             dependency.AssertWasCalled(x=>x.HelloMessage());
         }
                    
    }
}
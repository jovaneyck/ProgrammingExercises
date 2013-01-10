using NUnit.Framework;

namespace LanguageFeatures.Experiments.ExtensionMethods
{
    [TestFixture]
    class ExtensionMethodTest
    {
        [Test]
        public void ExtensionMethodsAreCool()
        {
            ExtensionUtilizer utilizer = new ExtensionUtilizer();
            Assert.AreEqual("Hello, I am an extension! Native: " + "NATIVE!", utilizer.MyExtension());
        }
    }


}

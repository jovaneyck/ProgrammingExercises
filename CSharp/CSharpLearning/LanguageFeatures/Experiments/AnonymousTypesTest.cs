using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LanguageFeatures.Experiments
{
    [TestFixture]
    class AnonymousTypesTest
    {
        [Test]
        public void AnonymousTypeDeclaration()
        {
            var anonymous = new {Id = 1337};
            Assert.AreEqual(1337, anonymous.Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WordWrapKata;

namespace WordWrapKata
{
    //Kata description: http://codingdojo.org/cgi-bin/wiki.pl?KataWordWrap
    [TestFixture]
    class WrapperTest
    {
        [Test]
        public void KeepsASpaceIntact()
        {
            string result = Wrapper.wrap(" ", 5);
            Assert.AreEqual(" ", result);
        }

        [Test]
        public void KeepsAShortWordIntact()
        {
            string word = "hello";
            string result = Wrapper.wrap(word, 5);

            Assert.AreEqual(word, result);
        }

        [Test]
        public void WrapsALongWord()
        {
            Assert.AreEqual("pan\ntry", Wrapper.wrap("pantry", 3));
        }
    }
}

using System;
using NUnit.Framework;

namespace WordWrapKata
{
    //Kata description: http://codingdojo.org/cgi-bin/wiki.pl?KataWordWrap
    [TestFixture]
    class WrapperTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "Column length", MatchType = MessageMatch.Contains)]
        public void CannotUseNegativeColumnLengths()
        {
            var wrapper = new Wrapper(0);
        }

        [Test]
        public void KeepsASpaceIntact()
        {
            string result = (new Wrapper(5)).Wrap(" ");
            Assert.AreEqual(" ", result);
        }

        [Test]
        public void KeepsAShortWordIntact()
        {
            string word = "hello";
            string result = (new Wrapper(5)).Wrap(word);

            Assert.AreEqual(word, result);
        }

        [Test]
        public void WrapsALongWord()
        {
            Assert.AreEqual("pan\ntry", (new Wrapper(3)).Wrap("pantry"));
        }

        [Test]
        public void WrapsAVeryLongWord()
        {
            Assert.AreEqual("pan\ntry\npan\ntry", (new Wrapper(3)).Wrap("pantrypantry"));
        }

        [Test]
        public void RemovesAWhiteSpaceOnAWrappingPosition()
        {
            Assert.AreEqual("wrap\nhere", (new Wrapper(4)).Wrap("wrap here"));
        }

        [Test]
        public void PreferWhiteSpaceOverWrappingMidWord()
        {
            Assert.AreEqual("wrap\nhere", (new Wrapper(6)).Wrap("wrap here"));
        }

        [Test]
        public void DontForceNewlinesWhenNotNecessary()
        {
            string stringToWrap = "do not wrap";
            Assert.AreEqual(stringToWrap, (new Wrapper(100)).Wrap(stringToWrap));
        }

        [Test]
        public void WrapALongSentence()
        {
            Assert.AreEqual("i am not\nsure if\nthis\nwill\nwork\ncorrectl\ny", (new Wrapper(8)).Wrap("i am not sure if this will work correctly"));
        }
    }
}

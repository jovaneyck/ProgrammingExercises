using System;
using NUnit.Framework;

namespace CodeWars.AreaOfACircle
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ShouldFailNegativeNumber()
        {
            Assert.Throws<ArgumentException>(delegate { Kata.CalculateAreaOfCircle("-123"); });
        }

        [Test]
        public void ShouldFailAlphaNumeric()
        {
            Assert.Throws<ArgumentException>(delegate { Kata.CalculateAreaOfCircle("number"); });
        }

        [Test]
        public void ShouldPass1()
        {
            Assert.AreEqual(5881.25, Kata.CalculateAreaOfCircle("43.2673"));
        }
    }
}
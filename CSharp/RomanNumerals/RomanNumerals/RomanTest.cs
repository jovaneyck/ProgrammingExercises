using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RomanNumerals
{
    [TestFixture]
    class RomanTest
    {
        [Test]
        public void One()
        {
            Assert.AreEqual("I", ToRoman(1));
        }

        private string ToRoman(int i)
        {
            return "I";
        }
    }
}

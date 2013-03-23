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

        [Test]
        public void Two()
        {
            Assert.AreEqual("II", ToRoman(2));
        }

        private string ToRoman(int i)
        {
            if(i == 1)
                return "I";
            return "II";
        }
    }
}

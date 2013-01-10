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
        public void ShouldKeepASpaceIntact()
        {
            string result = Wrapper.wrap(" ", 5);
            Assert.Equals(" ", result);
        }
    }
}

using NUnit.Framework;

namespace CodeWars.TheOldSwitcheroo
{
    public class Test
    {
        [Test]
        public void AcceptanceTest()
        {
            Assert.AreEqual(
                "208919 919 1 1215147 1920189147!! 161251195 [51431545] @30181853201225",
                Kata.Encode("this is a long string!! Please [encode] @C0RrEctly"));    
        }

        [TestCase("abc", "123")]
        [TestCase("ABCD", "1234")]
        [TestCase("ZzzzZ", "2626262626")]
        [TestCase("abc-#@5", "123-#@5")]
        public void FixedTest(string str, string expected)
        {
            Assert.AreEqual(expected, Kata.Encode(str));
        }
    }
}
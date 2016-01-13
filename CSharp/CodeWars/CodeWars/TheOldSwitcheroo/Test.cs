using NUnit.Framework;

namespace CodeWars.TheOldSwitcheroo
{
    public class Test
    {
        [Test]
        [TestCase("abc", "123")]
        [TestCase("ABCD", "1234")]
        [TestCase("ZzzzZ", "2626262626")]
        [TestCase("abc-#@5", "123-#@5")]
        [TestCase("this is a long string!! Please [encode] @C0RrEctly", "208919 919 1 1215147 1920189147!! 161251195 [51431545] @30181853201225")]
        public static void FixedTest(string str, string expected)
        {
            Assert.AreEqual(expected, Kata.Encode(str));
        }
    }
}
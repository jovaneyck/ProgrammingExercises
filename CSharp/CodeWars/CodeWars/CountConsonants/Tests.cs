using NUnit.Framework;

namespace CodeWars.CountConsonants
{

    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase("", 0)]
        [TestCase("aaaaa", 0)]
        [TestCase("Bbbbb", 5)]
        [TestCase("helLo world", 7)]
        [TestCase("h^$&^#$&^elLo world", 7)]
        [TestCase("012456789", 0)]
        [TestCase("012345_Cb", 2)]
        public static void FixedTest(string s, int result)
        {
            Assert.AreEqual(result, Kata.ConsonantCount(s));
        }
    }
}
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeWars.HowManyLicks
{
    [TestFixture]
    public class Tests
    {
        private Kata kata = new Kata();

        [Test]
        public void _0_BasicTests()
        {
            Assert.AreEqual(
                "It took 260 licks to get to the tootsie roll center of a tootsie pop. The toughest challenge was freezing temps.",
                kata.TotalLicks(new Dictionary<string, int> { { "freezing temps", 10 }, { "clear skies", -2 } }));

            Assert.AreEqual(
                "It took 245 licks to get to the tootsie roll center of a tootsie pop.", 
                kata.TotalLicks(new Dictionary<string, int> { { "happiness", -5 }, { "clear skies", -2 } }));

            Assert.AreEqual(
                "It took 252 licks to get to the tootsie roll center of a tootsie pop.", 
                kata.TotalLicks(new Dictionary<string, int>()));

            Assert.AreEqual(
                "It took 512 licks to get to the tootsie roll center of a tootsie pop. The toughest challenge was evil wizards.", 
                kata.TotalLicks(new Dictionary<string, int>() { { "dragons", 100 }, { "evil wizards", 110 }, { "trolls", 50 } }));

            Assert.AreEqual(
                "It took 2 licks to get to the tootsie roll center of a tootsie pop.", 
                kata.TotalLicks(new Dictionary<string, int>() { { "white magic", -250 } }));
        }
    }
}
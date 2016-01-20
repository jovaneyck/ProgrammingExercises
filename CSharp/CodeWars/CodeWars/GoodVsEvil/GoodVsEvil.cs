using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeWars.GoodVsEvil
{
    [TestFixture]
    public class GoodVsEvil
    {
        [Test]
        public static void ShouldBeATie()
        {
            Assert.AreEqual("Battle Result: No victor on this battle field", Kata.GoodVsEvil("1 0 0 0 0 0", "1 0 0 0 0 0 0"));
        }

        [Test]
        public static void EvilShouldWin()
        {
            Assert.AreEqual("Battle Result: Evil eradicates all trace of Good", Kata.GoodVsEvil("1 1 1 1 1 1", "1 1 1 1 1 1 1"));
        }

        [Test]
        public static void GoodShouldTriumph()
        {
            Assert.AreEqual("Battle Result: Good triumphs over Evil", Kata.GoodVsEvil("0 0 0 0 0 10", "0 1 1 1 1 0 0"));
        }
    }

    public class Kata
    {
        public static string GoodVsEvil(string good, string evil)
        {
            var goodWorth = Worth(good, new [] {1,2,3,3,4,10});
            var evilWorth = Worth(evil, new [] {1,2,2,2,3,5,10});
            return VictoryMessage(goodWorth, evilWorth);
        }

        private static int Worth(string worth, IList<int> strengths)
        {
            return worth
                .Split(' ')
                .Select(int.Parse)
                .Zip(strengths, (amount, strength) => amount * strength)
                .Sum();
        }

        private static string VictoryMessage(int goodWorth, int evilWorth)
        {
            if (goodWorth == evilWorth)
            {
                return "Battle Result: No victor on this battle field";
            }
            if (goodWorth > evilWorth)
            {
                return "Battle Result: Good triumphs over Evil";
            }

            return "Battle Result: Evil eradicates all trace of Good";
        }
    }
}
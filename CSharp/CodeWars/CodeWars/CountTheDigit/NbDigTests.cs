using System;
using System.Linq;
using NUnit.Framework;

namespace CodeWars.CountTheDigit
{
    [TestFixture]
    public class NbDigTests
    {
        [Test]
        public void test1()
        {
            Assert.AreEqual(4700, CountDig.NbDig(5750, 0));
            Assert.AreEqual(9481, CountDig.NbDig(11011, 2));
            Assert.AreEqual(7733, CountDig.NbDig(12224, 8));
            Assert.AreEqual(11905, CountDig.NbDig(11549, 1));
        }
    }

    public class CountDig
    {
        public static int NbDig(int n, int d)
        {
            return 
                Enumerable.Range(0, n + 1)
                    .Select(number => Math.Pow(number, 2))
                    .Select(number => $"{number}".ToCharArray())
                    .Select(number => number.Count(dig => dig == Convert.ToChar(d + 48)))
                    .Sum();
        }
    }
}
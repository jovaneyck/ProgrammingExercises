using System;
using System.Linq;

namespace CodeWars.Maximum_Length_Difference
{
    using NUnit.Framework;

    [TestFixture]
    public class MaxDiffLengthTests
    {
        [Test]
        public void emptyInputs()
        {
            Assert.AreEqual(-1, MaxDiffLength.Mxdiflg(new string[0], new string[0]));
            Assert.AreEqual(-1, MaxDiffLength.Mxdiflg(new string[0], new [] {"hello"}));
            Assert.AreEqual(-1, MaxDiffLength.Mxdiflg(new [] {"world"}, new string[0]));
        }

        [Test]
        public void test1()
        {
            string[] s1 = { "hoqq", "bbllkw", "oox", "ejjuyyy", "plmiis", "xxxzgpsssa", "xxwwkktt", "znnnnfqknaz", "qqquuhii", "dvvvwz" };
            string[] s2 = { "cccooommaaqqoxii", "gggqaffhhh", "tttoowwwmmww" };
            Assert.AreEqual(13, MaxDiffLength.Mxdiflg(s1, s2));
        }
    }

    public class MaxDiffLength
    {
        public static int Mxdiflg(string[] s1, string[] s2)
        {
            if (!s1.Any() || !s2.Any())
                return -1;

            return 
                (from first in s1
                 from second in s2
                 select Math.Abs(first.Length - second.Length))
                .Max();
        }
    }
}
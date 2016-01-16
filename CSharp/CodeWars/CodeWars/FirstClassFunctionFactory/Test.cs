using System;
using NUnit.Framework;

namespace CodeWars.FirstClassFunctionFactory
{
    [TestFixture]
    public class FactoryTest
    {
        [Test]
        public static void BasicTest()
        {
            var threes = FunctionFactory.factory(3);
            int[] myArr = { 1, 2, 3 };
            Assert.AreEqual(threes(myArr), new [] { 3, 6, 9 });
            var fives = FunctionFactory.factory(5);
            Assert.AreEqual(fives(myArr), new [] { 5, 10, 15 });
        }
    }
}
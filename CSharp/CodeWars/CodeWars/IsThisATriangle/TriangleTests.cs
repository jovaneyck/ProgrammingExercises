using NUnit.Framework;

namespace CodeWars.IsThisATriangle
{
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void IsTriangle_ValidPostiveNumbers_ReturnsTrue()
        {
            Assert.IsTrue(Triangle.IsTriangle(5, 7, 10));
        }
    }

    public class Triangle
    {
        public static bool IsTriangle(int a, int b, int c)
        {
            return
                a + b > c
                && b + c > a
                && a + c > b;
        }
    }
}
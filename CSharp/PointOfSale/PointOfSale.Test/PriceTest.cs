using NUnit.Framework;

namespace PointOfSale.Test
{
    [TestFixture]
    public class PriceTest
    {
        [Test]
        [TestCase(2.55, "2.55$", TestName = "Nominal Case")]
        [TestCase(100.01, "100.01$", TestName = "Larger numbers")]
        [TestCase(.01, "0.01$", TestName = "Leading zero, no zero")]
        [TestCase(0.01, "0.01$", TestName = "Leading zero, one zero")]
        [TestCase(00.01, "0.01$", TestName = "Leading zero, two zeroes")]
        [TestCase(.589, "0.59$", TestName = "Rounds up")]
        [TestCase(.599, "0.60$", TestName = "Rounds up more than one digit")]
        [TestCase(.601, "0.60$", TestName = "Rounds down")]
        public void AlwaysRoundedToTwoDecimalsWhenDisplayed(decimal input, string expected)
        {
            Assert.AreEqual(expected, new Price(input).ToString());
        }
    }
}
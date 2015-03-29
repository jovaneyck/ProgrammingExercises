using System;
using NUnit.Framework;

namespace PointOfSale.Test
{
    [TestFixture]
    public class PointOfSaleTest
    {
        [Test]
        public void RejectsAnInvalidPriceRegistry()
        {
            Assert.Throws<ArgumentNullException>(() => new PointOfSale(null));
        }
    }
}
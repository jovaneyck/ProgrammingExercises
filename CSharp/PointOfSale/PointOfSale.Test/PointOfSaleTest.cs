using System;
using NUnit.Framework;

namespace PointOfSale.Test
{
    [TestFixture]
    public class PointOfSaleTest
    {
        [Test]
        public void RejectsInvalidCollaborators()
        {
            Assert.Throws<ArgumentNullException>(() => new PointOfSale(null, new Display()));
            Assert.Throws<ArgumentNullException>(() => new PointOfSale(new PriceRegistry(), null));
        }
    }
}
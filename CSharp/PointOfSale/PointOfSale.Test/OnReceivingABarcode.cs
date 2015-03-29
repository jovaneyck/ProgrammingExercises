using NUnit.Framework;

namespace PointOfSale.Test
{
    [TestFixture]
    public class OnReceivingABarcode
    {
        [Test]
        public void DisplaysHelpfulMessageWhenScanningAnInvalidBarcode()
        {
            var pointOfSale = (new PointOfSale());
            pointOfSale.OnBarcode("");
            var message = pointOfSale.LastTextDisplayed;
            Assert.IsTrue(message.ToLower().Contains("invalid"));
        }
    }
}

using NUnit.Framework;

namespace PointOfSale.Test
{
    [TestFixture]
    public class OnReceivingABarcode
    {
        [Test]
        [TestCase("", TestName = "An empty string is invalid input")]
        [TestCase(null, TestName = "Null is invalid input")]
        public void DisplaysHelpfulMessageWhenScanningAnInvalidBarcode(string input)
        {
            var pointOfSale = (new PointOfSale());
            pointOfSale.OnBarcode(input);
            var message = pointOfSale.LastTextDisplayed;
            Assert.IsTrue(message.ToLower().Contains("invalid barcode"));
        }
    }
}

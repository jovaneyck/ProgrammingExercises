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
            var pointOfSale = (new PointOfSale(new PriceRegistry()));
            pointOfSale.OnBarcode(input);
            Assert.IsTrue(pointOfSale.LastTextDisplayed.ToLower().Contains("invalid barcode"));
        }

        [Test]
        public void DisplaysThePriceOfAValidBarcode()
        {
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("12345", 9.95m));
            pointOfSale.OnBarcode("12345");
            Assert.AreEqual("9.95$", pointOfSale.LastTextDisplayed);
        }

        [Test]
        public void DisplaysThePriceOfAValidBarcodeOfAnotherProduct()
        {
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("678910", 0.5m));
            pointOfSale.OnBarcode("678910");
            Assert.AreEqual("0.50$", pointOfSale.LastTextDisplayed);
        }

        [Test]
        public void DisplaysThePriceOfMultipleProductsInARow()
        {
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("12345", 0.3m).Register("678910", 0.5m));
            pointOfSale.OnBarcode("12345");
            Assert.AreEqual("0.30$", pointOfSale.LastTextDisplayed);
            
            pointOfSale.OnBarcode("678910");
            Assert.AreEqual("0.50$", pointOfSale.LastTextDisplayed);
        }
    }
}

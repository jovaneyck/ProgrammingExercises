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
            var display = new Display();
            var pointOfSale = new PointOfSale(new PriceRegistry(), display);
            pointOfSale.OnBarcode(input);
            Assert.IsTrue(display.LastTextDisplayed.ToLower().Contains("invalid barcode"));
        }

        [Test]
        public void DisplaysAWarningWhenTheBarcodeIsNotFound()
        {
            var display = new Display();
            var pointOfSale = new PointOfSale(new PriceRegistry(), display);
            pointOfSale.OnBarcode("555");

            Assert.AreEqual("no price found for barcode <555>", display.LastTextDisplayed.ToLower());
        }

        [Test]
        public void DisplaysThePriceOfAValidBarcode()
        {
            var display = new Display();
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("12345", 9.95m), display);
            pointOfSale.OnBarcode("12345");
            Assert.AreEqual("9.95$", display.LastTextDisplayed);
        }

        [Test]
        public void DisplaysThePriceOfAValidBarcodeOfAnotherProduct()
        {
            var display = new Display();
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("678910", 0.5m), display);
            pointOfSale.OnBarcode("678910");
            Assert.AreEqual("0.50$", display.LastTextDisplayed);
        }

        [Test]
        public void DisplaysThePriceOfMultipleProductsInARow()
        {
            var display = new Display();
            var pointOfSale = new PointOfSale(new PriceRegistry().Register("12345", 0.3m).Register("678910", 0.5m), display);
            
            pointOfSale.OnBarcode("12345");            
            pointOfSale.OnBarcode("678910");
            Assert.AreEqual("0.50$", display.LastTextDisplayed);
        }
    }
}


using System.Collections.Generic;
using System.Globalization;

namespace PointOfSale
{
    public class PointOfSale
    {
        private readonly Dictionary<string, decimal> _priceRegistry;

        public PointOfSale(Dictionary<string, decimal> priceRegistry)
        {
            _priceRegistry = priceRegistry;
        }

        public void OnBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                LastTextDisplayed = "Invalid barcode";
            }
            else
            {
                var price = _priceRegistry[barcode];
                LastTextDisplayed = ToPrettyPrintedPrice(price);
            }
        }

        private static string ToPrettyPrintedPrice(decimal price)
        {
            return price.ToString("0.00$", new NumberFormatInfo{NumberDecimalSeparator = "."});
        }

        public string LastTextDisplayed { get; private set; }
    }
}
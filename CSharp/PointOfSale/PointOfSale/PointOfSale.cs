
using System.Collections.Generic;

namespace PointOfSale
{
    public class PointOfSale
    {
        private readonly Dictionary<Barcode, Price> _priceRegistry;

        public PointOfSale(Dictionary<Barcode, Price> priceRegistry)
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
                LastTextDisplayed = price.ToString();
            }
        }

        public string LastTextDisplayed { get; private set; }
    }
}
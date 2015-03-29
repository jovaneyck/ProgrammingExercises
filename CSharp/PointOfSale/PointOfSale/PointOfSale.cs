

using System;

namespace PointOfSale
{
    public class PointOfSale
    {
        private readonly PriceRegistry _priceRegistry;

        public PointOfSale(PriceRegistry priceRegistry)
        {
            if (priceRegistry == null)
            {
                throw new ArgumentNullException("priceRegistry");
            }

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
                var price = _priceRegistry.PriceOf(barcode);
                LastTextDisplayed = price.ToString();
            }
        }

        public string LastTextDisplayed { get; private set; }
    }
}
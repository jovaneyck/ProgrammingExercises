

using System;

namespace PointOfSale
{
    public class PointOfSale
    {
        private readonly Display _display;
        private readonly PriceRegistry _priceRegistry;

        public PointOfSale(PriceRegistry priceRegistry, Display display)
        {
            if (priceRegistry == null)
            {
                throw new ArgumentNullException("priceRegistry");
            }

            _priceRegistry = priceRegistry;

            if (display == null)
            {
                throw new ArgumentNullException("display");
            }

            _display = display;
        }

        public void OnBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                _display.DisplayInvalidBarcodeMessage();
            }
            else if (! _priceRegistry.HasPriceFor(barcode))
            {
                _display.DisplayPriceNotFoundFor(barcode);
            }
            else
            {
                var price = _priceRegistry.PriceOf(barcode);
                _display.DisplayPrice(price);
            }
        }
    }
}
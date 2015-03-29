using System.Collections.Generic;

namespace PointOfSale
{
    public class PriceRegistry
    {
        private readonly Dictionary<Barcode, Price> _pricesForBarcode = new Dictionary<Barcode, Price>(); 

        public Price PriceOf(Barcode barcode)
        {
            return _pricesForBarcode[barcode];
        }

        public PriceRegistry Register(Barcode barcode, Price price)
        {
            _pricesForBarcode.Add(barcode, price);
            return this;
        }

        public bool HasPriceFor(string barcode)
        {
            return _pricesForBarcode.ContainsKey(barcode);
        }
    }
}
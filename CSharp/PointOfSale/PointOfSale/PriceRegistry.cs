using System.Collections.Generic;

namespace PointOfSale
{
    public class PriceRegistry
    {
        private readonly Dictionary<Barcode, Price> _priceForBarcode = new Dictionary<Barcode, Price>(); 

        public Price PriceOf(Barcode barcode)
        {
            return _priceForBarcode[barcode];
        }

        public PriceRegistry Register(Barcode barcode, Price price)
        {
            _priceForBarcode.Add(barcode, price);
            return this;
        }

        public bool HasPriceFor(string barcode)
        {
            return _priceForBarcode.ContainsKey(barcode);
        }
    }
}
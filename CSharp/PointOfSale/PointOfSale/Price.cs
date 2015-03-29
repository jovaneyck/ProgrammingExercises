using System.Globalization;

namespace PointOfSale
{
    public class Price
    {
        private readonly decimal _price;

        public Price(decimal price)
        {
            _price = price;
        }

        public override string ToString()
        {
            return _price.ToString("0.00$", new NumberFormatInfo{NumberDecimalSeparator = "."});
        }

        public static implicit operator Price(decimal p)
        {
            return new Price(p);
        }
    }
}
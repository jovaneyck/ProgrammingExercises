namespace PointOfSale
{
    public class Barcode
    {
        private readonly string _barcode;

        public Barcode(string source)
        {
            _barcode = source;
        }

        public static implicit operator Barcode(string source)
        {
            return new Barcode(source);
        }

        public override string ToString()
        {
            return _barcode;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Barcode))
            {
                return false;
            }
            return (obj as Barcode)._barcode == _barcode;
        }

        public override int GetHashCode()
        {
            return _barcode.GetHashCode();
        }
    }
}
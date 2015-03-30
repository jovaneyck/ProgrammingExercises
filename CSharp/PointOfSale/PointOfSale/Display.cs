namespace PointOfSale
{
    public class Display
    {
        public Display()
        {
            LastTextDisplayed = string.Empty;
        }

        public string LastTextDisplayed { get; private set; }

        public void DisplayPrice(Price price)
        {
            LastTextDisplayed = price.ToString();
        }

        public void DisplayPriceNotFoundFor(Barcode barcode)
        {
            LastTextDisplayed = string.Format("No price found for barcode <{0}>", barcode);
        }

        public void DisplayInvalidBarcodeMessage()
        {
            LastTextDisplayed = "Invalid barcode";
        }
    }
}
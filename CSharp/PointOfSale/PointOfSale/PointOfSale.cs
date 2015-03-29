
namespace PointOfSale
{
    public class PointOfSale
    {
        public void OnBarcode(string empty)
        {
            if (string.IsNullOrWhiteSpace(empty))
            {
                LastTextDisplayed = "Invalid barcode";
            }
            else
            {
                LastTextDisplayed = "9.95$";
            }
        }

        public string LastTextDisplayed { get; private set; }
    }
}
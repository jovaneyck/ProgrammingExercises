namespace VideoStore
{
    public class Movie
    {
        public const int CHILDRENS = 2;
        public const int REGULAR = 0;
        public const int NEW_RELEASE = 1;

        public readonly string Title;
        public readonly int PriceCode;

        public Movie(string title, int priceCode)
        {
            Title = title;
            PriceCode = priceCode;
        }
    }

}
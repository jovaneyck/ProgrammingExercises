namespace VideoStore
{
    public class Rental
    {
        public readonly Movie Movie;
        public readonly int DaysRented;

        public Rental(Movie movie, int daysRented)
        {
            Movie = movie;
            DaysRented = daysRented;
        }
    }

}
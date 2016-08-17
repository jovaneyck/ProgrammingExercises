using System.Collections.Generic;

namespace VideoStore
{
    public class Customer
    {
        public readonly string Name;
        private readonly IList<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            Name = name;
        }

        public void AddRental(Rental rental)
        {
            _rentals.Add(rental);
        }

        public string Statement()
        {
            double totalAmount = 0;
            var frequentRenterPoints = 0;
            var rentals = _rentals.GetEnumerator();
            var result = "Rental Record for " + Name + "\n";

            while (rentals.MoveNext())
            {
                double thisAmount = 0;
                var each = rentals.Current;

                // determines the amount for each line
                switch (each.Movie.PriceCode)
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.DaysRented > 2)
                            thisAmount += (each.DaysRented - 2)*1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.DaysRented*3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.DaysRented > 3)
                            thisAmount += (each.DaysRented - 3)*1.5;
                        break;
                }

                // add frequent renter points
                frequentRenterPoints++;

                // add bonus for a two day new release rental
                if (each.Movie.PriceCode == Movie.NEW_RELEASE && each.DaysRented > 1)
                    frequentRenterPoints++;

                // show figures for this rental
                result += "\t" + each.Movie.Title + "\t" + $"{thisAmount:F1}" + "\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += "You owed " + $"{totalAmount:F1}" + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";

            return result;
        }
    }
}
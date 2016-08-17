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
            var result = "Rental Record for " + Name + "\n";

            foreach (var rental in _rentals)
            {
                double thisAmount = 0;

                // determines the amount for each line
                switch (rental.Movie.PriceCode)
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (rental.DaysRented > 2)
                            thisAmount += (rental.DaysRented - 2)*1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += rental.DaysRented*3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (rental.DaysRented > 3)
                            thisAmount += (rental.DaysRented - 3)*1.5;
                        break;
                }

                // add frequent renter points
                frequentRenterPoints++;

                // add bonus for a two day new release rental
                if (rental.Movie.PriceCode == Movie.NEW_RELEASE && rental.DaysRented > 1)
                    frequentRenterPoints++;

                // show figures for this rental
                result += $"\t{rental.Movie.Title}\t{thisAmount:F1}\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += $"You owed {totalAmount:F1}\n";
            result += $"You earned {frequentRenterPoints} frequent renter points";

            return result;
        }
    }
}
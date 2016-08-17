using System;
using System.Collections.Generic;

namespace VideoStore
{
    public class Customer
    {
        private string _name;
        private IList<Rental> _rentals = new List<Rental>();

        public Customer(String name)
        {
            this._name = name;
        }

        public void addRental(Rental rental)
        {
            _rentals.Add(rental);
        }

        public String getName()
        {
            return _name;
        }

        public String statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            var rentals = _rentals.GetEnumerator();
            String result = "Rental Record for " + getName() + "\n";

            while (rentals.MoveNext())
            {
                double thisAmount = 0;
                Rental each = rentals.Current;

                // determines the amount for each line
                switch (each.getMovie().getPriceCode())
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.getDaysRented() > 2)
                            thisAmount += (each.getDaysRented() - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.getDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.getDaysRented() > 3)
                            thisAmount += (each.getDaysRented() - 3) * 1.5;
                        break;
                }

                // add frequent renter points
                frequentRenterPoints++;

                // add bonus for a two day new release rental
                if (each.getMovie().getPriceCode() == Movie.NEW_RELEASE && each.getDaysRented() > 1)
                    frequentRenterPoints++;

                // show figures for this rental
                result += "\t" + each.getMovie().getTitle() + "\t" + $"{thisAmount:F1}" + "\n";
                totalAmount += thisAmount;
            }

            // add footer lines
            result += "You owed " + $"{totalAmount:F1}" + "\n";
            result += "You earned " + frequentRenterPoints + " frequent renter points";

            return result;
        }
    }

}
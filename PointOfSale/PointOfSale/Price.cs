using System;
using System.Globalization;

namespace PointOfSale
{
    public class Price
    {
        private readonly long _priceInCents;

        public Price() 
            //Moq needs a parameterless ctor if you mock concrete types and this is NOT something you want to create an interface for.
            //Option A: introduce interfaces
            //Option B: live with this empty ctor and virtuals all over your methods
            //Option C: write integrated tests rather than isolated tests
            : this(0)
        {
        }

        public Price(long priceInCents)
        {
            _priceInCents = priceInCents;
        }

        //This might not be the best place to place formatting logic.
        public virtual string Formatted() //virtual: Moq needs to override this implementation
        {
            return 
                Decimal.Divide(_priceInCents, 100)
                    .ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
namespace FizzBuzz
{
    class FizzBuzz
    {
        public string FizzBuzzify(int number)
        {
            if (IsDivisibleByThree(number) && IsDivisibleByFive(number))
                return "FizzBuzz";
            if (IsFizz(number))
                return "Fizz";
            if (IsBuzz(number))
                return "Buzz";
            return number.ToString();
        }

        private bool IsFizz(int number)
        {
            return IsDivisibleByThree(number) || ContainsThree(number);
        }

        private bool IsBuzz(int number)
        {
            return IsDivisibleByFive(number) || ContainsFive(number);
        }

        private bool ContainsFive(int number)
        {
            return Contains(number, "5");
        }

        private bool Contains(int numberToCheck, string numberToContain)
        {
            return numberToCheck.ToString().Contains(numberToContain);
        }

        private bool ContainsThree(int number)
        {
            return Contains(number, "3");
        }

        private bool IsDivisibleByFive(int number)
        {
            return number % 5 == 0;
        }

        private bool IsDivisibleByThree(int number)
        {
            return number % 3 == 0;
        }
    }
}
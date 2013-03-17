namespace FizzBuzz
{
    class FizzBuzz
    {
        public string FizzBuzzify(int number)
        {
            if (IsDivisibleByThree(number) && IsDivisibleByFive(number))
                return "FizzBuzz";
            if (IsDivisibleByThree(number) || ContainsThree(number))
                return "Fizz";
            if (IsDivisibleByFive(number) || ContainsFive(number))
                return "Buzz";
            return number.ToString();
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
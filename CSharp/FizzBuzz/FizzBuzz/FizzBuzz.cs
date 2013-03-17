namespace FizzBuzz
{
    class FizzBuzz
    {
        public string FizzBuzzify(int number)
        {
            if (IsDivisibleByThree(number) && IsDivisibleByFive(number))
                return "FizzBuzz";
            if (IsDivisibleByThree(number))
                return "Fizz";
            if (IsDivisibleByFive(number))
                return "Buzz";
            return number.ToString();
        }

        private static bool IsDivisibleByFive(int number)
        {
            return number % 5 == 0;
        }

        private bool IsDivisibleByThree(int number)
        {
            return number % 3 == 0;
        }
    }
}
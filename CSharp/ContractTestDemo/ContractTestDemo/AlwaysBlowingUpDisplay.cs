using System;

namespace ContractTestDemo
{
    public class AlwaysBlowingUpDisplay : Display
    {
        public void DisplayPrice(int price)
        {
            throw new Exception("BOOM");
        }
    }
}
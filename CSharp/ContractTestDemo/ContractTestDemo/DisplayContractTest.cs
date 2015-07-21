using Xunit;

namespace ContractTestDemo
{
    public abstract class DisplayContractTest
    {
        [Fact]
        public void DoesNotBlowUpWhenDisplayingAPrice()
        {
            ADisplay().DisplayPrice(1337);
        }

        protected abstract Display ADisplay();
    }
}
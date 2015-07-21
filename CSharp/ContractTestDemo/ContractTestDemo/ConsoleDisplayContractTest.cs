
namespace ContractTestDemo
{
    public class ConsoleDisplayContractTest : DisplayContractTest
    {
        protected override Display ADisplay()
        {
            var display = new ConsoleDisplay();
            return display;
        }
    }
}

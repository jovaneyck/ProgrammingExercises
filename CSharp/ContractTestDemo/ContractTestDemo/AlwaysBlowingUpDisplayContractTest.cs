namespace ContractTestDemo
{
    public class AlwaysBlowingUpDisplayContractTest : DisplayContractTest
    {
        protected override Display ADisplay()
        {
            return new AlwaysBlowingUpDisplay();
        }
    }
}
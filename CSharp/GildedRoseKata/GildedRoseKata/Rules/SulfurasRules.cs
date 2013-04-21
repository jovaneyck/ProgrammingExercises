namespace GildedRoseKata.Rules
{
    public class SulfurasRules : ItemWithIncreasingQualityRules
    {
        protected override void DecreaseSellInOf(Item item)
        {
            //SellIn value does not decrease
        }

        public override void UpdateQualityOf(Item item)
        {
            //Quality cannot change
        }
    }
}
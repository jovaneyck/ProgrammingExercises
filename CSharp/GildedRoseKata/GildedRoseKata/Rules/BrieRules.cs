namespace GildedRoseKata.Rules
{
    public class BrieRules : ItemWithIncreasingQualityRules
    {
        protected override void UpdateQualityAfterSellInChange(Item item)
        {
            if (item.Quality < GildedRose.MaximumQuality)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
}
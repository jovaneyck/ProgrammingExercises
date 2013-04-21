namespace GildedRoseKata.Rules
{
    public class ItemWithIncreasingQualityRules : NormalItemRules
    {
        public override void UpdateQualityOf(Item item)
        {
            if (item.Quality < GildedRose.MaximumQuality)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
}
namespace GildedRoseKata.Rules
{
    public class ItemWithIncreasingQualityRules : NormalItemRules
    {
        public override void UpdateQuality(Item item, int maximumQuality)
        {
            if (item.Quality < maximumQuality)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
}
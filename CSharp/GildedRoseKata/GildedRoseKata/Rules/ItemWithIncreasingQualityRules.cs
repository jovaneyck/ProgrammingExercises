namespace GildedRoseKata.Rules
{
    public class ItemWithIncreasingQualityRules : Rules
    {
        public void UpdateQuality(Item item, int maximumQuality)
        {
            if (item.Quality < maximumQuality)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
}
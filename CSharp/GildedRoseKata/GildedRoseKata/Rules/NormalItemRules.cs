namespace GildedRoseKata.Rules
{
    public class NormalItemRules : Rules
    {
        public void UpdateQuality(Item item, int maximumQuality)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }
    }
}
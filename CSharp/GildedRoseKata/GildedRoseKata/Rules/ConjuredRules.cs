namespace GildedRoseKata.Rules
{
    internal class ConjuredRules : Rules
    {
        public void UpdateQuality(Item item, int maximumQuality)
        {
            item.Quality = item.Quality - 2;
        }
    }
}
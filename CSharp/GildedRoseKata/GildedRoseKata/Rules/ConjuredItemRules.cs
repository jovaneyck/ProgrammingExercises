namespace GildedRoseKata.Rules
{
    internal class ConjuredItemRules : NormalItemRules
    {
        public override void UpdateQuality(Item item, int maximumQuality)
        {
            item.Quality = item.Quality - 2;
        }
    }
}
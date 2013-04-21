namespace GildedRoseKata.Rules
{
    internal class ConjuredItemRules : NormalItemRules
    {
        public override void UpdateQualityOf(Item item)
        {
            item.Quality = item.Quality - 2;
        }
    }
}
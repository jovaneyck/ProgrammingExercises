namespace GildedRoseKata.Rules
{
    public class BrieRules : NormalItemRules
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
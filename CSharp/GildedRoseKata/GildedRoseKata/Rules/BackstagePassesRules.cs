namespace GildedRoseKata.Rules
{
    public class BackstagePassesRules : NormalItemRules
    {
        public override void UpdateQualityOf(Item item)
        {
            if (item.Quality < GildedRose.MaximumQuality)
            {
                item.Quality++;

                if (item.Quality < GildedRose.MaximumQuality)
                {
                    if (item.SellIn < 11)
                    {
                        item.Quality++;
                    }

                    if (item.SellIn < 6)
                    {
                        item.Quality++;
                    }
                }
            }
        }

        protected override void UpdateQualityUponPastSellIn(Item item)
        {
            item.Quality = 0;
        }
    }
}
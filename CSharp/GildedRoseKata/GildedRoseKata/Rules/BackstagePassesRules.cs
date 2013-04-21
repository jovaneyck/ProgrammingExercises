namespace GildedRoseKata.Rules
{
    public class BackstagePassesRules : NormalItemRules
    {
        public override void UpdateQuality(Item item, int maximumQuality)
        {
            if (item.Quality < maximumQuality)
            {
                item.Quality++;

                if (item.Quality < maximumQuality)
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
    }
}
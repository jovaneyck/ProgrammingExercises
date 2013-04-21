namespace GildedRoseKata.Rules
{
    public class NormalItemRules : Rules
    {
        public virtual void UpdateQuality(Item item, int maximumQuality)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        public void UpdateSellInOf(Item item)
        {
            DecreaseSellInOf(item);

            if (item.SellIn < 0)
            {
                if (item.Name == GildedRose.AgedBrie)
                {
                    if (item.Quality < GildedRose.MaximumQuality)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
                else
                {
                    if (item.Name != GildedRose.BackstagePasses && item.Quality > 0)
                    {
                        if (item.Name != GildedRose.Sulfuras)
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                    else
                    {
                        item.Quality = 0;
                    }
                }
            }
        }

        protected virtual void DecreaseSellInOf(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }
    }
}
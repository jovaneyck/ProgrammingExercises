using System.Collections.Generic;

namespace GildedRoseKata
{
    internal class GildedRose
    {
        private const string AgedBrie = "Aged Brie";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

        public const int MaximumQuality = 50;
        private readonly IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                UpdateQualityOf(item);
                UpdateSellInOf(item);
            }
        }

        private void UpdateQualityOf(Item item)
        {
            if (item.Name == BackstagePasses)
            {
                (new BackstagePassesRules()).UpdateQuality(item, MaximumQuality);
            }
            else if (item.Name == AgedBrie)
            {
                (new BrieRules()).UpdateQuality(item, MaximumQuality);
            }
            else if (item.Name == Sulfuras)
            {
                (new SulfurasRules()).UpdateQuality(item, MaximumQuality);
            }
            else
            {
                (new NormalItemRules()).UpdateQuality(item, MaximumQuality);
            }
        }

        private void UpdateSellInOf(Item item)
        {
            if (item.Name != Sulfuras)
            {
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < 0)
            {
                if (item.Name == AgedBrie)
                {
                    if (item.Quality < MaximumQuality)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
                else
                {
                    if (item.Name != BackstagePasses && item.Quality > 0)
                    {
                        if (item.Name != Sulfuras)
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

    }
}

public class Item
{
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
}

public class BackstagePassesRules
{
    public void UpdateQuality(Item item, int maximumQuality)
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

public class ItemWithIncreasingQuality
{
    public void UpdateQuality(Item item, int maximumQuality)
    {
        if (item.Quality < maximumQuality)
        {
            item.Quality = item.Quality + 1;
        }
    }
}

public class BrieRules : ItemWithIncreasingQuality
{
}

public class SulfurasRules : ItemWithIncreasingQuality
{
}

public class NormalItemRules
{
    public void UpdateQuality(Item item, int maximumQuality)
    {
        if (item.Quality > 0)
        {
            item.Quality = item.Quality - 1;
        }
    }
}
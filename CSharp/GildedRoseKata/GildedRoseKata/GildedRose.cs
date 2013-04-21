using System.Collections.Generic;
using GildedRoseKata;

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
                Rules rulesToApply = GetRulesFor(item);
                rulesToApply.UpdateQuality(item, MaximumQuality);
                UpdateSellInOf(item);
            }
        }

        private Rules GetRulesFor(Item item)
        {
            if (item.Name == BackstagePasses)
                return new BackstagePassesRules();
            if (item.Name == AgedBrie)
                return new BrieRules();
            if (item.Name == Sulfuras)
                return new SulfurasRules();
            return new NormalItemRules();
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

    interface Rules
    {
        void UpdateQuality(Item item, int maximumQuality);
    }
}

public class Item
{
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
}

public class BackstagePassesRules : Rules
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

public class BrieRules : ItemWithIncreasingQuality, Rules
{
}

public class SulfurasRules : ItemWithIncreasingQuality, Rules
{
}

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
using System.Collections.Generic;
using GildedRoseKata.Rules;

namespace GildedRoseKata
{
    /// <summary>
    /// Kata description can be found at: https://github.com/emilybache/Refactoring-Katas/tree/master/GildedRose
    /// </summary>
    internal class GildedRose
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        public const string Conjured = "Conjured";

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
                Rules.Rules rulesToApply = RulesFor(item);
                rulesToApply.UpdateQuality(item, MaximumQuality);
                rulesToApply.UpdateSellInOf(item);
            }
        }

        private Rules.Rules RulesFor(Item item)
        {
            if (item.Name == BackstagePasses)
                return new BackstagePassesRules();
            if (item.Name == AgedBrie)
                return new BrieRules();
            if (item.Name == Sulfuras)
                return new SulfurasRules();
            if (item.Name == Conjured)
                return new ConjuredItemRules();
            return new NormalItemRules();
        }
    }
}

public class Item
{
    public string Name { get; set; }

    public int SellIn { get; set; }

    public int Quality { get; set; }
}
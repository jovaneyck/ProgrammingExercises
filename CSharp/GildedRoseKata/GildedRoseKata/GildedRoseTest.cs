using System.Collections.Generic;
using NUnit.Framework;

namespace GildedRoseKata
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void UpdateQualityDoesNotChangeName()
        {
            const string uniqueName = "uniqueName";
            IList<Item> items = new List<Item> {new Item {Name = uniqueName, SellIn = 0, Quality = 0}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(uniqueName, items[0].Name);
        } 
        
        [Test]
        public void AdvancingADayLowersBothQualityAndSellIn()
        {
            const int initialSellIn = 100;
            const int initialQuality = 100;
            IList<Item> items = new List<Item> {new Item {Name = "item", SellIn = initialSellIn, Quality = initialQuality}};

            GildedRose app = new GildedRose(items);
            app.UpdateQuality();
            
            Assert.IsTrue(items[0].SellIn < initialSellIn);
            Assert.IsTrue(items[0].Quality < initialQuality);
        }

        [Test]
        public void SellInDecreasesByOne()
        {
            const int initialSellIn = 100;
            IList<Item> items = new List<Item> { new Item { Name = "item", SellIn = initialSellIn, Quality = 100 } };

            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            Assert.AreEqual(items[0].SellIn, initialSellIn - 1);
        }

        [Test]
        public void QualityDecreasesByOne()
        {
            const int initialQuality = 100;
            IList<Item> items = new List<Item> { new Item { Name = "item", SellIn = 100, Quality = initialQuality } };

            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            Assert.AreEqual(items[0].Quality, initialQuality - 1);
        }



    }
}
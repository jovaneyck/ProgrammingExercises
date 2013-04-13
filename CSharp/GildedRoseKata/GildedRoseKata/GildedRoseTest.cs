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
            Item item = CreateAndAdvanceSingleItem(uniqueName, 0, 0);
            Assert.AreEqual(uniqueName, item.Name);
        }

        private Item CreateAndAdvanceSingleItem(string uniqueName, int sellInValue, int quality)
        {
            IList<Item> items = new List<Item> {new Item {Name = uniqueName, SellIn = sellInValue, Quality = quality}};
            GildedRose app = new GildedRose(items);
            app.UpdateQuality();

            return items[0];
        }

        [Test]
        public void AdvancingADayLowersBothQualityAndSellIn()
        {
            const int initialSellIn = 100;
            const int initialQuality = 100;

            Item item = CreateAndAdvanceSingleItem("item", initialSellIn, initialQuality);

            Assert.IsTrue(item.SellIn < initialSellIn);
            Assert.IsTrue(item.Quality < initialQuality);
        }

        [Test]
        public void SellInDecreasesByOne()
        {
            const int initialSellIn = 100;
            Item item = CreateAndAdvanceSingleItem("item", initialSellIn, 100);

            Assert.AreEqual(item.SellIn, initialSellIn - 1);
        }

        [Test]
        public void QualityDecreasesByOne()
        {
            const int initialQuality = 100;
            Item item = CreateAndAdvanceSingleItem("item", 100, initialQuality);

            Assert.AreEqual(item.Quality, initialQuality - 1);
        }



    }
}
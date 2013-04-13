using System.Collections.Generic;
using NUnit.Framework;

namespace GildedRoseKata
{
    [TestFixture]
    public class GildedRoseTest
    {
        private const int SellInFactor = 1;
        private const int QualityFactor = 1;

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
        public void SellInDecreasesByFactor()
        {
            const int initialSellIn = 100;
            Item item = CreateAndAdvanceSingleItem("item", initialSellIn, 100);

            Assert.AreEqual(initialSellIn - SellInFactor, item.SellIn);
        }

        [Test]
        public void QualityDecreasesByFactor()
        {
            const int initialQuality = 100;
            Item item = CreateAndAdvanceSingleItem("item", 100, initialQuality);

            Assert.AreEqual(initialQuality - QualityFactor, item.Quality);
        }

        [Test]
        public void WhenSellInHasPassedQualityDegradesTwiceAsFast()
        {
            const int initialQuality = 100;
            const int expiredSellIn = 0;

            Item item = CreateAndAdvanceSingleItem("item", expiredSellIn, initialQuality);

            Assert.AreEqual(initialQuality - 2*QualityFactor, item.Quality);
        }

        [Test]
        public void QualityDoesNotBecomeNegative()
        {
            const int lowestQuality = 0;
            Item item = CreateAndAdvanceSingleItem("item", 100, lowestQuality);

            Assert.AreEqual(lowestQuality, item.Quality);
        }

        [Test]
        public void AgedBrieQualityIncreases()
        {
            const int initialQuality = 10;
            Item item = CreateAndAdvanceSingleItem("Aged Brie", 100, initialQuality);

            Assert.AreEqual(initialQuality + SellInFactor, item.Quality);
        }

        [Test]
        public void AgedBrieQualityIncreasesAfterSellIn()
        {
            const int lowestQuality = 0;
            Item item = CreateAndAdvanceSingleItem("Aged Brie", 0, lowestQuality);

            Assert.AreEqual(lowestQuality + 2, item.Quality); 
            //What's this? I expected a 1... Instead of degrading twice as fast, its quality gets raised by a factor two?
        }        
        
        [Test]
        public void QualityCannotBeMoreThanFifty()
        {
            const int maximumQuality = 50;
            //Brie is the only item I currently know of that rises in quality, so we'll use that one to test
            Item item = CreateAndAdvanceSingleItem("Aged Brie", 1, maximumQuality);

            Assert.AreEqual(maximumQuality, item.Quality); 
        }

        [Test]
        public void LegendaryItemHasNoSellInOrQualityDecrease()
        {
            const int sellIn = 100;
            const int quality = 50;
            Item item = CreateAndAdvanceSingleItem("Sulfuras, Hand of Ragnaros", sellIn, quality);
            //Had to go into the source code for the correct name

            Assert.AreEqual(sellIn, item.SellIn);
            Assert.AreEqual(quality, item.Quality); 
        }

        [Test]
        public void BackstagePassesDecreaseInQualityWayAheadOfTheConcert()
        {
            const int initialQuality = 50;
            Item item = CreateAndAdvanceSingleItem("Backstage passes", 100, initialQuality);

            //Again, not according to how I interpreted the specs
            Assert.AreEqual(initialQuality - QualityFactor, item.Quality); 
        }
    }
}
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
            var app = new GildedRose(items);
            app.UpdateQuality();
            Assert.AreEqual(uniqueName, items[0].Name);
        }
    }
}
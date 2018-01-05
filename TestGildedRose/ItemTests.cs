using System;
using Legacy;
using NUnit.Framework;

namespace TestGildedRose
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void ItemTypeConversionTests()
        {
            Assert.AreEqual(ItemType.Normal, ItemType.GetItemType("Any Item"));
            Assert.AreEqual(ItemType.AgedBrie, ItemType.GetItemType("Aged Brie"));
            Assert.AreEqual(ItemType.BackstagePasses, ItemType.GetItemType("Backstage passes to a TAFKAL80ETC concert"));
            Assert.AreEqual(ItemType.Sulfuras, ItemType.GetItemType("Sulfuras, Hand of Ragnaros"));
        }
    }
}
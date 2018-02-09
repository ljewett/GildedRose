using System;
using System.Data;
using NUnit.Framework;
using Legacy;
using Moq;

namespace TestGildedRose
{
        
    [TestFixture]
    public class Tests
    {
        private Mock<IDatabaseConnector> dbMock;
        private DataSet items;

        public void AddTestItem(object name, object sellin, object quality)
        {
            items.Tables[0].Rows.Add(name, sellin, quality);
        }

        public Item GetTestItem(int row = 0)
        {
            Item item = new Item(items.Tables[0].Rows[row]);
            return item;
        }

        public void UpdateQuality(int times = 1)
        {
            while (times > 0)
            {
                GildedRose.updateQuality(dbMock.Object);
                times--;
            }
        }
        
        [SetUp]
        public void MockDatabaseSetUp()
        {
            dbMock = new Mock<IDatabaseConnector>();
            items = new DataSet();
            DataTable table = new DataTable("gildedrose");
            
            table.Columns.Add(new DataColumn("name", typeof(string)));
            table.Columns.Add(new DataColumn("sellin", typeof(int)));
            table.Columns.Add(new DataColumn("quality", typeof(int)));
            items.Tables.Add(table);
            
            dbMock.Setup(x => x.GetItems()).Returns(items);
        }
        
        [Test]
        public void UpdateQualityWithMock()
        {
            string name = "+5 Dexterity Vest";
            AddTestItem(name, "10", "20");
            UpdateQuality(2);
            Item item = GetTestItem();
            
            Assert.AreEqual(8, item.sellin);
            Assert.AreEqual(name, item.name);
            Assert.AreEqual(ItemType.Normal, item.type);
        }
        
        [Test]
        public void UpdateQualityDecreaseQualityToZero()
        {
            string name = "+5 Dexterity Vest";
            AddTestItem(name, "1", "1");
            UpdateQuality(2);
            Item item = GetTestItem();

            Assert.AreEqual(-1, item.sellin);
            Assert.AreEqual(0, item.quality);
        }
        
        [Test]
        public void SulfurasNeverDecreases()
        {
            AddTestItem(ItemType.Sulfuras, "10", "80");
            UpdateQuality(2);
            GildedRose.updateQuality(dbMock.Object);
            Item item = GetTestItem();
            
            Assert.AreEqual(10, item.sellin);
            Assert.AreEqual(80, item.quality);
        }

        [Test]
        public void DoubleQualityDegredationAfterZeroSellin()
        {
            AddTestItem(ItemType.Normal, "1", "10");
            UpdateQuality();
            Item item = GetTestItem();
            
            Assert.AreEqual(9, item.quality);
            Assert.AreEqual(0, item.sellin);

            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(7, item.quality);
            Assert.AreEqual(-1, item.sellin);
        }

        [Test]
        public void AgedbrieIncreasesQualityToFifty()
        {
            AddTestItem(ItemType.AgedBrie, "10", "49");
            UpdateQuality();
            Item item = GetTestItem();
            
            Assert.AreEqual(50, item.quality);
            Assert.AreEqual(9, item.sellin);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(50, item.quality);
            Assert.AreEqual(8, item.sellin);
        }
        
        [Test]
        public void BackStagePassesIncreasesNormallyAboveTenSellinDays()
        {   
            AddTestItem(ItemType.BackstagePasses, "12", "30");

            UpdateQuality();
            Item item = GetTestItem();
            Assert.AreEqual(31, item.quality);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(32, item.quality);

        }
        
        [Test]
        public void BackStagePassesDoubleIncreasesBelowTenSellinDays()
        {
            AddTestItem(ItemType.BackstagePasses, "10", "32");
            UpdateQuality();
            Item item = GetTestItem();
            Assert.AreEqual(34, item.quality);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(36, item.quality);
        }

        [Test]
        public void BackStagePassesTripleIncreasesBelowFiveSellinDaysMaxesAtFifty()
        {
            AddTestItem(ItemType.BackstagePasses, "5", "42");
            UpdateQuality();
            Item item = GetTestItem();
            Assert.AreEqual(45, item.quality);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(48, item.quality);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(50, item.quality);
        }

        [Test]
        public void BackStagePassesValueGoesToZeroAfterSellin()
        {
            AddTestItem(ItemType.BackstagePasses, "1", "50");
            UpdateQuality();
            Item item = GetTestItem();
            Assert.AreEqual(50, item.quality);
            
            UpdateQuality();
            item = GetTestItem();
            Assert.AreEqual(0, item.quality);
        }
    }
}
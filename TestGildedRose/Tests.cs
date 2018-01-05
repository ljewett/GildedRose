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
            items.Tables[0].Rows.Add("+5 Dexterity Vest", "10", "20");
            GildedRose.updateQuality(dbMock.Object);
            GildedRose.updateQuality(dbMock.Object);
            Assert.AreEqual(8, (int)items.Tables[0].Rows[0][1]);
        }
        
        [Test]
        public void UpdateQualityDecreaseQualityToZero()
        {
            items.Tables[0].Rows.Add("+5 Dexterity Vest", "1", "1");
            GildedRose.updateQuality(dbMock.Object);
            GildedRose.updateQuality(dbMock.Object);
            Assert.AreEqual(-1, (int)items.Tables[0].Rows[0][1]);
            Assert.AreEqual(0, (int)items.Tables[0].Rows[0][2]);
        }
        
        [Test]
        public void SulfurasNeverDecreases()
        {
            items.Tables[0].Rows.Add("Sulfuras, Hand of Ragnaros", "10", "80");
            GildedRose.updateQuality(dbMock.Object);
            GildedRose.updateQuality(dbMock.Object);
            Assert.AreEqual(10, (int)items.Tables[0].Rows[0][1]);
            Assert.AreEqual(80, (int)items.Tables[0].Rows[0][2]);
        }
    }
}
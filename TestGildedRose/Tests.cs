using System;
using NUnit.Framework;
using Legacy;
using Moq;

namespace TestGildedRose
{
        
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void MockDatabaseConnection()
        {
            var dbMock = new Mock<DatabaseConnector>();
        }
        
        [Test]
        public void CreateGildedRose()
        {
            GildedRose gildedRose = new GildedRose();
            Assert.True(true);
        }
    }
}
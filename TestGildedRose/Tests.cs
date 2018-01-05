using System;
using NUnit.Framework;
using Legacy;

namespace TestGildedRose
{
        
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateGildedRose()
        {
            GildedRose gildedRose = new GildedRose();
            Assert.True(true);
        }
    }
}
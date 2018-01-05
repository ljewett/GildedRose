using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Win32.SafeHandles;

namespace Legacy
{
    
    public sealed class ItemType
    {
        private readonly string name;
        private readonly int value;
        
        public static readonly ItemType Normal = new ItemType(1, "Normal");
        public static readonly ItemType Sulfuras = new ItemType(2, "Sulfuras, Hand of Ragnaros");
        public static readonly ItemType AgedBrie = new ItemType(3, "Aged Brie");
        public static readonly ItemType BackstagePasses = new ItemType(4, "Backstage passes to a TAFKAL80ETC concert");
        
        private static readonly ItemType[] _itemTypes =
        {
            Normal, Sulfuras, AgedBrie, BackstagePasses
        };
        
        
        private ItemType(int value, string name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return this.name;
        }

        public static ItemType GetItemType(string value)
        {
            foreach (ItemType v in _itemTypes)
            {
                if (value.Equals(v.ToString(), StringComparison.OrdinalIgnoreCase))
                    return v;
            }
            return Normal;
        }
    }
}
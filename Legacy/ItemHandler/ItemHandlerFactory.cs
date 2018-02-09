using System;
using System.Collections.Generic;

namespace Legacy
{
    public class ItemHandlerFactory
    {
        private Dictionary<ItemType, Func<ItemHandler>> map = new Dictionary<ItemType, Func<ItemHandler>>
            {
                {ItemType.AgedBrie, () => new AgedBrieItemHandler()},
                {ItemType.BackstagePasses, () => new BackstagePassItemHandler()},                
                {ItemType.Sulfuras, () => new SulfurasItemHandler()},
            }
            ;
        
        public ItemHandler getItemHandler(ItemType itemType)
        {
            if (map.ContainsKey(itemType))
            {
                var itemHandler = map[itemType]();
                return itemHandler;
            }
            return new DefaultItemHandler();

            
/*
            if (itemType == ItemType.AgedBrie)
            {
                outputHandler = new AgedBrieItemHandler();
            }
            else if (itemType == ItemType.BackstagePasses)
            {
                outputHandler = new BackstagePassItemHandler();
            }
            else if (itemType == ItemType.Sulfuras)
            {
                outputHandler = new SulfurasItemHandler();
            }
*/
//            else if (itemType == ItemType.Normal)
//            {
//                outputHandler = new DefaultItemHandler();
//            }
//            return outputHandler;
        }
    }
}
namespace Legacy
{
    public class ItemHandlerFactory
    {
        public ItemHandler getItemHandler(ItemType itemType)
        {
            ItemHandler outputHandler = null;
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
            else if (itemType == ItemType.Normal)
            {
                outputHandler = new DefaultItemHandler();
            }
            return outputHandler;
        }
    }
}
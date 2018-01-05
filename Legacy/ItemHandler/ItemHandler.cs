using System;

namespace Legacy
{
    public abstract class ItemHandler
    {
        public virtual Item Update(Item item)
        {
            return item;
        }

        public int increaseQuality(int quality, int value)
        {
            return Math.Min(quality + value, 50);
        }

        public int decreaseQuality(int quality, int value)
        {
            return Math.Max(quality - value, 0);
        }
    }
}
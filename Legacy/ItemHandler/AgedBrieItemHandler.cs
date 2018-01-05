namespace Legacy
{
    public class AgedBrieItemHandler : ItemHandler
    {
        public override Item Update(Item item)
        {
            item.quality = this.increaseQuality(item.quality, 1);
            item.sellin -= 1;
            return item;
        }
    }
}
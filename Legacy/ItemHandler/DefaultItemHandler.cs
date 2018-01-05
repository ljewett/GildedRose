namespace Legacy
{
    public class DefaultItemHandler : ItemHandler
    {
        public override Item Update(Item item)
        {
            int decrease = item.sellin > 0 ? 1 : 2;
            item.quality = this.decreaseQuality(item.quality, decrease);
            item.sellin -= 1;
            return item;
        }
    }
}
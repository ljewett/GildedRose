using System.Data;
    
namespace Legacy
{
    public class Item
    {
        public int sellin;
        public int quality;
        public string name;
        public ItemType type;
        
        public Item(DataRow data)
        {
            this.name = (string)data[0];
            this.sellin = (int)data[1];
            this.quality = (int)data[2];
            this.type = ItemType.GetItemType(name);
        }
    }
}
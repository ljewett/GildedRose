namespace Legacy
{
    public class BackstagePassItemHandler : ItemHandler
    {
        private enum SellInValue
        {
            DoubleValue,
            TripleValue,
            DefaultValue,
            ZeroValue
        }

        private static SellInValue getValue(int sellin)
        {
            if (sellin <= 0)
            {
                return SellInValue.ZeroValue;
            } else if (sellin < 6)
            {
                return SellInValue.TripleValue;
            } else if (sellin < 11)
            {
                return SellInValue.DoubleValue;
            }
            return SellInValue.DefaultValue;
        }

        public override Item Update(Item item)
        {
            switch (getValue(item.sellin))
            {
                case SellInValue.DefaultValue:
                    item.quality = this.increaseQuality(item.quality, 1);
                    break;
                case SellInValue.DoubleValue:
                    item.quality = this.increaseQuality(item.quality, 2);
                    break;
                case SellInValue.TripleValue:
                    item.quality = this.increaseQuality(item.quality, 3);
                    break;
                case SellInValue.ZeroValue:
                    item.quality = 0;
                    break;
            }

            item.sellin -= 1;
            return item;
        }
    }
}
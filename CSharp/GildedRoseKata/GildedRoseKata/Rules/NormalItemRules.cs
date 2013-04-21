namespace GildedRoseKata.Rules
{
    public class NormalItemRules : Rules
    {
        public virtual void UpdateQualityOf(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        public void UpdateSellInOf(Item item)
        {
            DecreaseSellInOf(item);
            HandleNegativeSellinValue(item);
        }

        protected virtual void DecreaseSellInOf(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private void HandleNegativeSellinValue(Item item)
        {
            if (item.SellIn < 0)
            {
                UpdateQualityUponPastSellIn(item);
            }
        }

        protected virtual void UpdateQualityUponPastSellIn(Item item)
        {
            if (item.Quality <= 0)
            {
                item.Quality = 0;
            }
            else
            {
                UpdateQualityOf(item);   
            }
        }
    }
}
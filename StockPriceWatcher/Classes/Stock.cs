namespace StockPriceWatcher.Classes
{
    internal class Stock
    {
        private string symbol;
        private decimal regularMarketPrice;
        private decimal buyPrice;
        private decimal sellPrice;

        public string Symbol { get => symbol; set => symbol = value; }
        public decimal RegularMarketPrice { get => regularMarketPrice; set => regularMarketPrice = value; }
        public decimal BuyPrice { get => buyPrice; set => buyPrice = value; }
        public decimal SellPrice { get => sellPrice; set => sellPrice = value; }

        public Stock(string stockName, decimal regularMarketPrice, decimal sellPrice, decimal buyPrice) 
        {
            this.symbol = stockName;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
            this.regularMarketPrice = regularMarketPrice;
        }
    }
}

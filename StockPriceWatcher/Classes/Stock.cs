namespace StockPriceWatcher.Classes
{
    internal class Stock
    {
        private string symbol;
        private double regularMarketPrice;
        private double buyPrice;
        private double sellPrice;

        public string Symbol { get => symbol; set => symbol = value; }
        public double RegularMarketPrice { get => regularMarketPrice; set => regularMarketPrice = value; }
        public double BuyPrice { get => buyPrice; set => buyPrice = value; }
        public double SellPrice { get => sellPrice; set => sellPrice = value; }

        public Stock(string stockName, double regularMarketPrice, double sellPrice, double buyPrice) 
        {
            this.symbol = stockName;
            this.sellPrice = sellPrice;
            this.buyPrice = buyPrice;
            this.regularMarketPrice = regularMarketPrice;
        }
    }
}

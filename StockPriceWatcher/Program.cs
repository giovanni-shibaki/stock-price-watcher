namespace StockPriceWatcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            IStockObservable.StockWatcher stockObserver = new IStockObservable.StockWatcher("9tigA99iz7nUXg4FQmbGFH");
            Stock stock = new Stock("PETR4", 0, 20, 30);

            await stockObserver.GetStock(client, stock);

        }
    }
}
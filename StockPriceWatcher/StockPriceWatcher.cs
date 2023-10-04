using Microsoft.Extensions.Configuration;

namespace StockPriceWatcher
{
    class StockPriceWatcher
    {
        static async Task Main(string[] args)
        {
            // Initial config to check for arguments, and set config variables
            InitialConfiguration configVariables;
            try {
                configVariables = new InitialConfiguration(args);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            MailHandler mailHandler = new MailHandler(configVariables.SMTPServer!, Int32.Parse(configVariables.SMTPPort!), configVariables.SMTPUsername!, configVariables.SMTPPassword!);

            // HttpClient to send requests to the API
            HttpClient client = new HttpClient();

            StockMonitor stockMonitor = new StockMonitor(configVariables.ApiKey!, configVariables.UpdateDelay);

            StockReporterSubscriber stockReporterSubscriber = new StockReporterSubscriber(mailHandler, stockMonitor);
            stockReporterSubscriber.Subscribe(configVariables.EmailsToNotify!);

            // Initiate the Watcher
            Stock stock = new Stock(configVariables.StockSymbol!, 0, configVariables.SellPrice, configVariables.BuyPrice);
            await stockMonitor.GetStock(client, stock);
        }
    }
}
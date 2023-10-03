namespace StockPriceWatcher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Check for arguments
            if(args.Length != 3)
            {
                Console.WriteLine("Insuficient arguments!\nNeeded: 3\nUsage: program.exe <Stock Symbol> <Sell Price> <Buy Price>");
                return;            
            }

            // Check for config file
            String path = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName;
            if(!(File.Exists($@"{path}\Config\emails.txt") && File.Exists($@"{path}\Config\smtpConfig.txt")))
            {
                Console.WriteLine("No configuration files found! Please check if both emails.txt and smtpConfig.txt files are inside the Config directory");
                return;
            }

            // Read 

            HttpClient client = new HttpClient();

            IStockObservable.StockWatcher stockObserver = new IStockObservable.StockWatcher("9tigA99iz7nUXg4FQmbGFH", 1800000);
            Stock stock = new Stock("PETR4", 0, 20, 30);

            await stockObserver.GetStock(client, stock);

        }
    }
}
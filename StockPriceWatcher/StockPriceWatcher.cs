using Microsoft.Extensions.Configuration;

namespace StockPriceWatcher
{
    class StockPriceWatcher
    {
        static async Task Main(string[] args)
        {
            // Initial config to check for arguments, and set config variables
            InitialConfig configVariables = new InitialConfig();

            // Check for arguments
            if (args.Length != 3)
            {
                Console.WriteLine("Insuficient arguments!\nNeeded: 3\nUsage: program.exe <Stock Symbol> <Sell Price> <Buy Price>");
                return;            
            }

            // Get program arguments
            string stockSymbol;
            decimal sellPrice;
            decimal buyPrice;
            try
            {
                stockSymbol = args[0];
                sellPrice = Decimal.Parse(args[1]);
                buyPrice = Decimal.Parse(args[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error converting arguments!");
                return;
            }

            // Reading config file and setting mailHandler
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot root = builder.Build();
            IConfigurationSection SMTPSettingsSection = root.GetSection("SMTPSettings");
            //Console.WriteLine(SMTPSettingsSection["Server"]);

            MailHandler mailHandler = new MailHandler(SMTPSettingsSection["Server"]!, Int32.Parse(SMTPSettingsSection["Port"]!), SMTPSettingsSection["Username"]!, SMTPSettingsSection["Password"]!);

            // HttpClient to send requests to the API
            HttpClient client = new HttpClient();

            // ApiKey and updateDelay come from the Configuration file
            string ApiKey = root.GetSection("APIToken").Value!;
            int updateDelay = Int32.Parse(root.GetSection("updateDelay").Value!);

            StockMonitor stockWatcher = new StockMonitor(ApiKey, updateDelay);
            Stock stock = new Stock(stockSymbol, 0, sellPrice, buyPrice);

            // Now, set the list of emails to subscribe to the observable
            IConfigurationSection EmailsSection = root.GetSection("emails");

            IEnumerable<IConfigurationSection> enumEmails = EmailsSection.GetChildren();
            List<string> emailsToNotify = new List<string>();
            foreach (var email in enumEmails)
            {
                new StockReporter(mailHandler, email.Value!).Subscribe(stockWatcher);
            }

            // Initiate the Watcher
            await stockWatcher.GetStock(client, stock);
        }
    }
}
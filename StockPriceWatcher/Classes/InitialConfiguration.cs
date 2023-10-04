using Microsoft.Extensions.Configuration;

namespace StockPriceWatcher.Classes
{
    internal class InitialConfiguration
    {
        public List<string>? EmailsToNotify { get; set; }
        public string? ApiKey { get; set; }
        public int UpdateDelay { get; set; }
        public string? SMTPServer { get; set; }
        public string? SMTPPort { get; set; }
        public string? SMTPUsername { get; set; }
        public string? SMTPPassword { get; set; }
        public string? StockSymbol { get; set; }
        public decimal SellPrice { get; set; }
        public decimal BuyPrice { get; set; }

        public InitialConfiguration(string[] args)
        {
            if (args.Length != 3)
            {
                throw new Exception("Insuficient arguments!\nNeeded: 3\nUsage: program.exe <Stock Symbol> <Sell Price> <Buy Price>");
            }

            try
            {
                this.StockSymbol = args[0];
                this.SellPrice = Decimal.Parse(args[1]);
                this.BuyPrice = Decimal.Parse(args[2]);
            }
            catch (Exception ex)
            {
                throw new Exception("Error converting arguments!");
            }

            // Reading config file and setting mailHandler
            IConfigurationBuilder builder;
            IConfigurationRoot root;
            try {
                builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
                root = builder.Build();
            } catch(Exception ex)
            {
                throw new Exception("Configuration not found! (appsettings.json).");
            }
            IConfigurationSection SMTPSettingsSection = root.GetSection("SMTPSettings");

            this.SMTPServer = SMTPSettingsSection["Server"];
            this.SMTPPort = SMTPSettingsSection["Port"];
            this.SMTPUsername = SMTPSettingsSection["Username"];
            this.SMTPPassword = SMTPSettingsSection["Password"];

            this.ApiKey = root.GetSection("APIToken").Value!;
            this.UpdateDelay = Int32.Parse(root.GetSection("updateDelay").Value!);

            // Now, set the list of emails to subscribe to the observable
            IConfigurationSection EmailsSection = root.GetSection("emails");

            IEnumerable<IConfigurationSection> enumEmails = EmailsSection.GetChildren();
            this.EmailsToNotify = new List<string>();
            foreach (var email in enumEmails)
            {
                this.EmailsToNotify.Add(email.Value!);
            }
        }
    }
}

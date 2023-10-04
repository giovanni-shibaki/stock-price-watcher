namespace StockPriceWatcher.Classes
{
    internal class InitialConfig
    {
        public List<string>? EmailsToNotify { get; set; }
        public string? ApiKey { get; set; }
        public int UpdateDelay { get; set; }
        public string? SMTPServer { get; set; }
        public string? SMTPPort { get; set; }
        public string? SMTPUsername { get; set; }
        public string? SMTPPassword { get; set; }

        public InitialConfig()
        {

        }
    }
}

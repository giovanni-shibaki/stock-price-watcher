namespace StockPriceWatcher.Classes
{
    internal class StockReporterSubscriber
    {
        public MailHandler MailHandler { get; set; }
        public StockMonitor StockMonitor { get; set; }

        public StockReporterSubscriber(MailHandler mailHandler, StockMonitor stockMonitor)
        {
            this.MailHandler = mailHandler;
            this.StockMonitor = stockMonitor;
        }

        public void Subscribe(List<string> emails)
        {
            foreach (string email in emails)
            {
                new StockReporter(this.MailHandler, email).Subscribe(this.StockMonitor);
            }
        }
    }
}

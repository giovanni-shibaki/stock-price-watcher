using MimeKit;

namespace StockPriceWatcher.Classes
{
    internal class Config
    {
        public List<string>? AlertEmails { get; set; }

        public Config(string emailsFilePath, string smtpConfigFilePath)
        {

        }
    }
}

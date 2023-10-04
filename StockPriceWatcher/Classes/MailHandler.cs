namespace StockPriceWatcher.Classes
{
    internal class MailHandler
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public MailHandler(string server, int port, string username, string password) 
        { 
            this.Username = username;
            this.Server = server;
            this.Port = port;
            this.Password = password;
        }

        public void sendEmail(string to, Stock stock)
        {
            MimeMessage email = new MimeMessage();

            email.From.Add(new MailboxAddress("", this.Username));
            email.To.Add(new MailboxAddress("", to));

            email.Subject = "Stock Price Alert";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text =  $"<p>Dear {to},</p>\r\n" +
                        $"<p>The stock price for symbol {stock.Symbol} has reached an important threshold:</p>\r\n" +
                        $"<ul>\r\n<li><strong>Current Price:</strong> {stock.RegularMarketPrice}</li>\r\n" +
                        $"<li><strong>Buy Price:</strong> {stock.BuyPrice}</li>\r\n<li><strong>" +
                        $"Sell Price:</strong> {stock.SellPrice}</li>\r\n</ul>\r\n" +
                        $"<p>Please take appropriate action based on the current market conditions.</p>\r\n" +
                        $"<p>Thank you for using our stock price alert service.</p>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(this.Server, this.Port, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate(this.Username, this.Password);

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}

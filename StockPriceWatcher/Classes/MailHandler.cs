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

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<b>Hello all the way from the land of C#</b>"
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

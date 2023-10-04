// Reference: https://learn.microsoft.com/pt-br/dotnet/standard/events/how-to-implement-an-observer

namespace StockPriceWatcher.Interfaces
{
    internal interface IStockObserver
    {
        public class StockObserver : IObserver<Stock>
        {
            private IDisposable? unsubscriber;
            private bool first = true;
            private Stock? last;
            private MailHandler mailHandler;
            private string email;

            public StockObserver(MailHandler mailHandler, string email)
            {
                this.mailHandler = mailHandler;
                this.email = email;
            }

            public virtual void Subscribe(IObservable<Stock> provider)
            {
                unsubscriber = provider.Subscribe(this);
            }

            public virtual void Unsubscribe()
            {
                unsubscriber!.Dispose();
            }

            public virtual void OnCompleted()
            {
                Console.WriteLine("Additional stock data will not be transmitted.");
            }

            public virtual void OnError(Exception error)
            {
                // Do nothing
            }

            public virtual void OnNext(Stock currentStock)
            {
                Console.WriteLine($"OnNext() of {this.email}");

                if(first)
                {
                    last = currentStock;
                    first = false;

                    mailHandler.sendEmail(this.email, currentStock);
                }
                else
                {
                    mailHandler.sendEmail(this.email, currentStock);
                }
            }
        }
    }
}

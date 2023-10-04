using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceWatcher.Classes
{
    internal class StockReporter : IObserver<Stock>
    {
        private IDisposable? unsubscriber;
        private bool first = true;
        private Stock? last;
        private MailHandler mailHandler;
        private string email;

        public StockReporter(MailHandler mailHandler, string email)
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

            if (first)
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

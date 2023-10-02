// Reference: https://learn.microsoft.com/pt-br/dotnet/standard/events/how-to-implement-an-observer

namespace StockPriceWatcher.Interfaces
{
    internal interface IStockObserver
    {
        public class StockObserver : IObserver<Stock>
        {
            private IDisposable unsubscriber;
            private bool first = true;
            private Stock last;

            public virtual void Subscribe(IObservable<Stock> provider)
            {
                unsubscriber = provider.Subscribe(this);
            }

            public virtual void Unsubscribe()
            {
                unsubscriber.Dispose();
            }

            public virtual void OnCompleted()
            {
                Console.WriteLine("Additional stock data will not be transmitted.");
            }

            public virtual void OnError(Exception error)
            {
                // Do nothing
            }

            public virtual void OnNext(Stock value)
            {
                Console.WriteLine("Stock data: ");

                if(first)
                {
                    last = value;
                    first = false;
                }
                else
                {
                    // Change
                }
            }
        }
    }
}

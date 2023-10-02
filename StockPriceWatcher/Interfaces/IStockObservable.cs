using System;
using System.Linq;

namespace StockPriceWatcher.Interfaces
{
    internal interface IStockObservable
    {
        public class StockWatcher : IObservable<Stock>
        {
            private List<IObserver<Stock>> observers;
            public StockWatcher()
            {
                observers = new List<IObserver<Stock>>();
            }

            private class Unsubscriber : IDisposable
            {
                private List<IObserver<Stock>> observers;
                private IObserver<Stock> observer;

                public Unsubscriber(List<IObserver<Stock>> observers, IObserver<Stock> observer)
                {
                    this.observers = observers;
                    this.observer = observer;
                }

                public void Dispose()
                {
                    if (!(observer == null)) observers.Remove(observer);
                }
            }

            public IDisposable Subscribe(IObserver<Stock> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);

                return new Unsubscriber(observers, observer);
            }

            public void GetStock()
            {
                // Get Stock Prices from API
            }
        }
    }
}

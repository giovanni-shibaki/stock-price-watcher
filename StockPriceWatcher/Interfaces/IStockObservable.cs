using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace StockPriceWatcher.Interfaces
{
    internal interface IStockObservable
    {
        public class StockWatcher : IObservable<Stock>
        {
            private List<IObserver<Stock>> observers;
            private string token;

            // TODO: Input updateDelay from config file
            private readonly int updateDelay = 1800000; // 1800000 miliseconds = 30 minutes (This API free version only updates the data every 30 minutes)
            public StockWatcher(string token)
            {
                this.observers = new List<IObserver<Stock>>();
                this.token = token;
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

            public async Task GetStock(HttpClient client, Stock stock)
            {
                string queryURL = string.Format("https://brapi.dev/api/quote/{0}?token={1}&fundamental=true&dividends=false", stock.Symbol, this.token);
                JsonNode JsonResult = null;

                try
                {
                    HttpResponseMessage res = await client.GetAsync(queryURL);
                    res.EnsureSuccessStatusCode();
                    string result = await res.Content.ReadAsStringAsync();

                    JsonNode stockNode = JsonNode.Parse(result)!;

                    JsonResult = stockNode!["results"]![0]!;

                    if (JsonResult == null)
                        throw new Exception("Error parsing result JSON");
                }
                catch (Exception ex)
                {

                }

                //Console.WriteLine($"{JsonResult!["regularMarketPrice"]}\n");

                
            }
        }
    }
}

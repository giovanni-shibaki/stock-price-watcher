using System.Net;
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
            private readonly int updateDelay = 1800000; // 1800000 miliseconds = 30 minutes (This API free version only updates the data every 30 minutes)
            
            public StockWatcher(string token, int updateDelay)
            {
                this.observers = new List<IObserver<Stock>>();
                this.token = token;
                this.updateDelay = updateDelay;
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
                SymbolQuotation? symbolQ = new SymbolQuotation();

                while(true)
                {
                    try
                    {
                        HttpResponseMessage res = await client.GetAsync(queryURL);

                        if (res.StatusCode == HttpStatusCode.OK) // Sucess -> Will return all the data from the chosen symbol
                        {
                            string result = await res.Content.ReadAsStringAsync();

                            JsonNode stockNode = JsonNode.Parse(result)!;

                            JsonSerializerOptions options = new JsonSerializerOptions()
                            {
                                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                            };

                            symbolQ = JsonSerializer.Deserialize<SymbolQuotation>(stockNode!["results"]![0]!.ToJsonString(), options);
                        }
                        else if (res.StatusCode == HttpStatusCode.NotFound) // Did not find the chosen symbol
                        {
                            Console.WriteLine($"Could not find input symbol '{stock.Symbol}'. Please verify the inputed symbol\n");
                            return;
                        }
                        else // HttpStatusCode.BadRequest -> Invalid token / API key
                        {
                            Console.WriteLine($"Invalid token / key! Please verify the configuration file\n");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}\n");
                    }

                    // Compare the current price (regularMarketPrice) with the sell / buy prices
                    stock.RegularMarketPrice = symbolQ!.regularMarketPrice;

                    if(stock.RegularMarketPrice < stock.BuyPrice || stock.RegularMarketPrice > stock.SellPrice)
                    {
                        Console.WriteLine($"Current Regular Market Price is lower than buyPrice ({stock.BuyPrice}) or higher than sellPrice ({stock.SellPrice}). Current Regular Market Price: {stock.RegularMarketPrice}. Sending emails...");
                        foreach (var observer in observers.ToArray())
                        {
                            if(observer != null)
                            {
                                observer.OnNext(stock);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No need to notify. Current Regular Market Price: {stock.RegularMarketPrice}");
                    }

                    // Now sleep for updateDelay minutes
                    Thread.Sleep(updateDelay);
                }
            }
        }
    }
}

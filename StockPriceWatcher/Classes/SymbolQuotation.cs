namespace StockPriceWatcher.Classes
{
    internal class SymbolQuotation
    {
        public string? currency { get; set; }
        public decimal twoHundredDayAverage { get; set; }
        public decimal twoHundredDayAverageChange { get; set; }   
        public decimal? twoHundredDayAverageChangePercent { get; set; } // During tests, Percent fields can return with null
        public Int64 marketCap { get; set; }
        public string? shortName { get; set; }
        public string? longName { get; set; }
        public decimal regularMarketChange { get; set; }
        public decimal? regularMarketChangePercent { get; set; } // During tests, Percent fields can return with null
        public string? regularMarketTime { get; set; }
        public decimal regularMarketPrice { get; set; }
        public decimal regularMarketDayHigh { get; set; }
        public string? regularMarketDayRange { get; set; }
        public decimal regularMarketDayLow { get; set; }
        public int regularMarketVolume { get; set; }
        public decimal regularMarketPreviousClose { get; set; }
        public decimal regularMarketOpen { get; set; }
        public int averageDailyVolume3Month { get; set; }
        public int averageDailyVolume10Day { get; set; }
        public decimal fiftyTwoWeekLowChange { get; set; }
        public decimal? fiftyTwoWeekLowChangePercent { get; set; } // During tests, Percent fields can return with null
        public string? fiftyTwoWeekRange { get; set; }
        public decimal fiftyTwoWeekHighChange { get; set; }
        public decimal? fiftyTwoWeekHighChangePercent { get; set; } // During tests, Percent fields can return with null
        public decimal fiftyTwoWeekLow { get; set; }
        public decimal fiftyTwoWeekHigh { get; set; }
        public string? symbol { get; set; }
        public decimal priceEarnings { get; set; }
        public decimal earningsPerShare { get; set; }
        public string? logourl { get; set; }
    }
}

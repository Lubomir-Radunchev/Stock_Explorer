using Azure;
using Newtonsoft.Json;

namespace Stock_Explorer.DTO
{
    public class StockYahooDTO
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        public string Region { get; set; }
        public string QuoteType { get; set; }
        public bool Triggerable { get; set; }
        public string QuoteSourceName { get; set; }
        public double RegularMarketPrice { get; set; }
        public long RegularMarketTime { get; set; }
        public double RegularMarketChange { get; set; }
        public double RegularMarketOpen { get; set; }
        public double RegularMarketDayHigh { get; set; }
        public double RegularMarketDayLow { get; set; }
        public long RegularMarketVolume { get; set; }
        public string ShortName { get; set; }
        public string Currency { get; set; }
        public string MarketState { get; set; }
        public bool EsgPopulated { get; set; }
        // ... add more properties as needed

        // Additional properties for completeness
        public double RegularMarketChangePercent { get; set; }
        public string RegularMarketDayRange { get; set; }
        public double RegularMarketPreviousClose { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public int BidSize { get; set; }
        public int AskSize { get; set; }
        public string MessageBoardId { get; set; }
        public string FullExchangeName { get; set; }
        public string LongName { get; set; }
        public string FinancialCurrency { get; set; }
        public long AverageDailyVolume3Month { get; set; }
        public long AverageDailyVolume10Day { get; set; }
        public double FiftyTwoWeekLowChange { get; set; }
        public double FiftyTwoWeekLowChangePercent { get; set; }
        public string FiftyTwoWeekRange { get; set; }
        public double FiftyTwoWeekHighChange { get; set; }
        public double FiftyTwoWeekHighChangePercent { get; set; }
        public double FiftyTwoWeekLow { get; set; }

        public double AdjClose{ get; set; }
        public double Close { get; set; }
    }


    public class StockInfo
    {
        public QuoteResponse QuoteResponse { get; set; }
    }

    public class QuoteResponse
    {
        public List<StockYahooDTO> Result { get; set; }
    }
}

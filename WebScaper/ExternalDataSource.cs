using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DataCollector
{
    public class ExternalDataSourceOutputData<T>
    {
        public T Data { get; }

        public ExternalDataSourceOutputData(T data)
        {
            Data = data;
        }
    }

    public static class FinvizDataSource
    {
        public static string WebSiteUri = "";
        public static ScrapingInstruction GetScrapingInstructions(StockSearchInputObject input)
        {
            return new ScrapingInstruction()
            {

            };
        }

        public class InfoUnit
        {
            public string Number { get; set; }
            public string Ticker { get; set; }
            public string Company { get; set; }
            public string Sector { get; set; }
            public string Industry { get; set; }
            public string Country { get; set; }
            public string MarketCap { get; set; }
            public string Pe { get; set; }
            public string Price { get; set; }
            public string Change { get; set; }
            public string Volume { get; set; }
        }
    }

    public static class YahooFinanceDataSource
    {
        public class InfoUnit
        {
            public DateTime EarningsDate { get; set; }
        }
    }

    public class ScrapingInstruction
    {
        public string DomainUri { get; }
        public NameValueCollection NameValueCollection { get; }
    }
}

using System;
using System.Collections.Generic;

namespace DataCollector
{
    public class StockInfoLoader
    {
        private readonly IWebScraperCommandProvider _webScraperCommandProvider;

        public StockInfoLoader(IWebScraperCommandProvider webScraperCommandProvider)
        {
            _webScraperCommandProvider = webScraperCommandProvider;
        }

        public IList<StockInfoObj> GetStockList(StockSearchInputObject serchInput)
        {
            throw new NotImplementedException();
        }
    }

    public class StockInfoObj
    {
        public StockInfoObj(string ticker, string company, string sector, string industry,
            string country, string marketCap, string pe, string price, string change, string volume, DateTime earningsDate, bool isEtf)
        {
            Ticker = ticker;
            Company = company;
            Sector = sector;
            Industry = industry;
            Country = country;
            Price = price;
            Change = change;
            Volume = volume;
            EarningsDate = earningsDate;
            IsEtf = isEtf;
        }

        public string Ticker { get; }
        public string Company { get; }
        public string Sector { get; }
        public string Industry { get; }
        public string Country { get; }
        public string Price { get; }
        public string Change { get; }
        public string Volume { get; }
        public DateTime EarningsDate { get; }
        public bool IsEtf { get; }
    }

    public class StockSearchInputObject
    {
        public bool OnlyOptionable { get; }
        public int? MinimumPrice { get; }
        public int? MinimumCurrentVolume { get; }
        public bool? PriceAboveOrBelowSimpleMovingAverages { get; private set; }
        public int MinimumDaysEarningDateDistance { get; }

        private StockSearchInputObject(bool onlyOptionable, int? minimumPrice, int? minimumCurrentVolume, 
            bool? priceAboveOrBelowSimpleMovingAverages, int minimumDaysEarningDateDistance)
        {
            OnlyOptionable = onlyOptionable;
            MinimumCurrentVolume = minimumCurrentVolume;
            MinimumPrice = minimumPrice;
            PriceAboveOrBelowSimpleMovingAverages = priceAboveOrBelowSimpleMovingAverages;
            MinimumDaysEarningDateDistance = minimumDaysEarningDateDistance;
        }

        public static class Builder
        {
            public static StockSearchInputObject Optionable(int days)
            {
                return new StockSearchInputObject(true, 100, 1000000, null, days);
            }

            public static StockSearchInputObject WithPriceAboveSimpleMovingAverages(StockSearchInputObject obj)
            {
                obj.PriceAboveOrBelowSimpleMovingAverages = true;
                return obj;
            }

            public static StockSearchInputObject WithPriceBelowSimpleMovingAverages(StockSearchInputObject obj)
            {
                obj.PriceAboveOrBelowSimpleMovingAverages = false;
                return obj;
            }

        }
        
    }
}

using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using DataCollector;
using DeepEqual;
using DeepEqual.Syntax;

namespace DataCollector.Test
{
    //public class MockFinvizData : FinvizDataSource.OutputData
    //{
    //    public MockFinvizData(IEnumerable<FinvizDataSource.InfoUnit> dataList) : base(dataList) { }
    //    //public IEnumerable<ExternalDataSource.Finviz.OutputData.InfoUnit> DataList { get; set; }   
    //}

    //public class MockYahooData : YahooFinanceDataSource.OutputData
    //{
    //    public MockYahooData(YahooFinanceDataSource.OutputData.InfoUnit data) : base(data) { }
    //}

    public class StockInfoLoaderTest
    {
        private StockInfoLoader _sut;
        private Mock<IWebScraperCommandProvider> _webScaper;
        private static readonly int OptionableMinLimitDays = 50;
        private static readonly int DaysEarningsDateForExclusion = OptionableMinLimitDays - 2;
        private static readonly int GoodDaysEarningsDate = OptionableMinLimitDays + 2;


        [SetUp]
        public void Setup()
        {
            _webScaper = new Mock<IWebScraperCommandProvider>();            
            _sut = new StockInfoLoader(_webScaper.Object);
        }

        [Test]
        public void Should_GetStockInfoData()
        {
            //arrange
            var serchInput = StockSearchInputObject.Builder.Optionable(days: OptionableMinLimitDays);
            var allStockList = GenerateStockList();
            var ticketsWithAcceptableEarningsDate = allStockList.GetRange(0, 4);
            MockScrapers(ticketsWithAcceptableEarningsDate.Select(i => i.Ticker).ToList(), allStockList);

            //act
            var expected = ticketsWithAcceptableEarningsDate.Select(i => new StockInfoObj());
            var actual = _sut.GetStockList(serchInput);

            //assert
            expected.ShouldDeepEqual(actual);
        }


        private void MockScrapers(IList<string> ticketsWithAccepatbleEarningsDate,
            IList<FinvizDataSource.InfoUnit> stockListFromFinviz)
        {
            _webScaper.Setup(i => i.GetScrapingCommand<YahooFinanceDataSource.InfoUnit>())
                .Returns(si => new ExternalDataSourceOutputData<YahooFinanceDataSource.InfoUnit>(new YahooFinanceDataSource.InfoUnit
                {
                    EarningsDate = 
                        DateTime.Today.AddDays(
                            OptionableMinLimitDays + 
                            (ticketsWithAccepatbleEarningsDate.Contains(
                                si.NameValueCollection[0]) ? GoodDaysEarningsDate : DaysEarningsDateForExclusion))
                })
            );

            _webScaper.Setup(i => i.GetScrapingCommand<IList<FinvizDataSource.InfoUnit>>())
                .Returns((si) => new ExternalDataSourceOutputData<IList<FinvizDataSource.InfoUnit>>(stockListFromFinviz));
        }


        private static List<FinvizDataSource.InfoUnit> GenerateStockList()
        {
            //finviz scrape command
            return new List<FinvizDataSource.InfoUnit>()
            {
                new FinvizDataSource.InfoUnit()
                {
                    Number = "",
                    Ticker = "",
                    Company = "",
                    Sector = "",
                    Industry = "",
                    Country = "",
                    MarketCap = "",
                    Pe = "",
                    Price = "",
                    Volume = "",
                    Change = ""                    
                },
            };
        }
    }
}

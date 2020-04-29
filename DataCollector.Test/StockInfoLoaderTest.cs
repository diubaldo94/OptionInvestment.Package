using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DataCollector.Test
{
    public class MockFinvizData : ExternalDataSource.Finviz.OutputData
    {

    }

    public class MockYahooData : ExternalDataSource.YahooFinance.OutputData
    {

    }

    public class StockInfoLoaderTest
    {
        private readonly StockInfoLoader _sut;
        private readonly Mock<IWebScraperCommandProvider> _webScaper;

        [SetUp]
        public void Setup()
        {
            _webScaper = Mock.Of<IWebScraperCommandProvider>();

            //finviz scrape command
            Func<ScrapingInstruction, ExternalDataSource.Finviz.OutputData> getDataFromFinviz =
                (si) => new MockFinvizData();
            _webScaper.Setup(i => i.GetScrapingCommand(It.Is<ExternalDataSource>(s => s.Name == ExternalDataSource.Finviz.Name)))
                .Returns(getDataFromFinviz);

            //yahoo scrape command
            Func<ScrapingInstruction, ExternalDataSource.Yahoo.OutputData> getDataFromYahoo =
                (si) => new MockYahooData();
            _webScaper.Setup(i => i.GetScrapingCommand(It.Is<ExternalDataSource>(s => s.Name == ExternalDataSource.Yahoo.Name)))
                .Returns(getDataFromYahoo);

            _sut = new StockInfoLoader(_webScaper.Object);
        }

        [Test]
        public void GetStockInfoData()
        {
            //arrange
            var serchInput = StockSearchInputObject.Optionable(days: 50);            

            //act
            var stockList = _sut.GetStockList(serchInput);
            var expected = new List<StockInfoObj>()
            {

            };

            //assert
            Assert.AreEqual
        }
    }
}

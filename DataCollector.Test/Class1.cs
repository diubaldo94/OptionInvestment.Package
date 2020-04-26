using Moq;
using NUnit.Framework;
using System;

namespace DataCollector.Test
{
    public class StockInfoLoaderTest
    {
        private readonly StockInfoLoader _sut;
        private readonly IWebScraper _webScaper;

        [SetUp]
        public void Setup()
        {
            _webScaper = Mock.Of<IWebScraper>();
            _sut = new StockInfoLoader(_webScaper);
        }

        [Test]
        public void GetStockInfoData()
        {

        }
    }
}

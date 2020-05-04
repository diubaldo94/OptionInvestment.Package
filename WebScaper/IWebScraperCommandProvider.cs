using System;

namespace DataCollector
{
    public interface IWebScraperCommandProvider
    {
        Func<ScrapingInstruction, ExternalDataSourceOutputData<T>> GetScrapingCommand<T>();
    }

    //public class WebScraperCommandProvider : Iw
    //{

    //}
}

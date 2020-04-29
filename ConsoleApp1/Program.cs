using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var tt = GetAllStockInfo();
            var t = 1;
        }

        private static IList<StockInfoObj> GetAllStockInfo()
        {
            var list = new List<StockInfoObj>();
            int iii = 0;
            while (true)
            {
                try
                {
                    list.AddRange(GetStockInfo(iii++));
                }
                catch(EndOfResultsException e)
                {
                    break;
                }
                catch(Exception e)
                {
                    throw;
                }
            }
            return list;
        }

        private static IEnumerable<StockInfoObj> GetStockInfo(int i)
        {
            var baseFinvizUrl = @"https://www.finviz.com/screener.ashx";
            var uriBuilder = new UriBuilder(baseFinvizUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query, Encoding.ASCII);

            query["v"] = "111";
            query["f"] = "sh_curvol_o1000,sh_opt_option,sh_price_o100"
                //+ ",ta_sma20_pb,ta_sma200_pb,ta_sma50_pb"
                ;
            query["ft"] = "3";
            query["r"] = (1 * 20*i).ToString();
            uriBuilder.Query = HttpUtility.UrlDecode(query.ToString());
            var longurl = uriBuilder.ToString();

            /*
             differenza tra bearish e bullish è
             tasma20 pb e pa
             
            //bearish
            var filePath = @"https://www.finviz.com/screener.ashx?
   v=111&
   f=sh_curvol_o1000,sh_opt_option,sh_price_o100,ta_sma20_pb,ta_sma200_pb,ta_sma50_pb&
   ft=3";

            //bullish
            var filePath = @"https://www.finviz.com/screener.ashx?
   v=111&
   f=sh_curvol_o1000,sh_opt_option,sh_price_o100,ta_sma20_pa,ta_sma200_pa,ta_sma50_pa
   &ft=3";
            */

            var doc = new HtmlWeb();
            var nodes = doc.Load(longurl).DocumentNode
                .SelectSingleNode("//*[@id=\"screener-content\"]/table/tr[4]/td/table")
               //    /table[2]/tbody/tr[4]/td/table/tbody
               .Descendants("tr").Skip(1)
               ;
            //var tt = nodes.FirstChild.FirstChild
            //    .Descendants().ToArray()[3]
            //     //.Select(y => y.Descendants().Where(x => x.Attributes["class"].Value == "box"))
            //     //.ToList();
            //     ;
            var list = new List<StockInfoObj>();
            if (nodes.Count() < 2)
                throw new EndOfResultsException();
            foreach (var node in nodes)
            {
                var texts = node.Descendants("#text").Select(p => p.InnerHtml).Where(l => !l.Equals("\n")).ToArray();
                //list.AddRange();
                var stockInfo = new StockInfoObj()
                {
                    Number = texts[0],
                    Ticker = texts[1],
                    Company = texts[2],
                    Sector = texts[3],
                    Industry = texts[4],
                    Country = texts[5],
                    MarketCap = texts[6],
                    Pe = texts[7],
                    Price = texts[8],
                    Change = texts[9],
                    Volume = texts[10],
                    EarningsDate = texts[4] == "Exchange Traded Fund" ? DateTime.MaxValue : GetEarningsDate(texts[1])
                };
                list.Add(stockInfo);
            }

            return list.Where(p => p.EarningsDate > DateTime.Today.AddDays(50));
        }

        private static DateTime GetEarningsDate(string ticket)
        {
            try
            {
                string innerHtml = new HtmlWeb().Load($"https://finance.yahoo.com/quote/" + ticket).DocumentNode
                    .SelectSingleNode("//*[@id=\"quote-summary\"]/div[2]/table/tbody/tr[5]/td[2]/span").InnerHtml;
                return DateTime.Parse(innerHtml);
            }
            catch(Exception e)
            {
                return DateTime.MinValue;
            }
        }
        //  /table/tbody/tr[5]/td[2]/span
        public class StockInfoObj
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
            public DateTime EarningsDate { get; set; }
        }
    }
}

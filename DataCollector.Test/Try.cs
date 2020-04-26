using HtmlAgilityPack;
using NUnit.Framework;
using System.Linq;

namespace DataCollector.Test
{
    public class Try
    {
        [Test]
        public void cazz()
        {
            var filePath = @"FinvizHtml.txt";
            var doc = new HtmlDocument();
            doc.Load(filePath);
            var nodes = doc.DocumentNode.SelectSingleNode("screener-content").FirstChild.FirstChild
                .Descendants().ToArray()[3]
                 //.Select(y => y.Descendants().Where(x => x.Attributes["class"].Value == "box"))
                 //.ToList();
                 ;
            var t = 1;
        }
    }
}

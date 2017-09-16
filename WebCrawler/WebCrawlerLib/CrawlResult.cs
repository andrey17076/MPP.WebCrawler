using System.Collections.Generic;

namespace WebCrawlerLib
{
    public sealed class CrawlResult
    {
        public CrawlResult(string link) : this(link, new List<CrawlResult>())
        {
        }

        public CrawlResult(string link, List<CrawlResult> nestedResults)
        {
            Link = link;
            NestedResults = nestedResults;
        }

        public string Link { get; set; }
        public List<CrawlResult> NestedResults { get; set; }
    }
}

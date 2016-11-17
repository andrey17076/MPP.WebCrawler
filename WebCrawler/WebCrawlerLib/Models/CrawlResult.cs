using System;
using System.Collections.Generic;

namespace WebCrawlerLib.Models
{
    public sealed class CrawlResult
    {

        public CrawlResult() : this(string.Empty)
        {
        }

        public CrawlResult(string link) : this(link, new List<CrawlResult>())
        {
        }

        public CrawlResult(string link, List<CrawlResult> nestedLinks)
        {
            Link = link;
            NestedLinks = nestedLinks;
        }

        public string Link { get; set; }
        public List<CrawlResult> NestedLinks { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace WebCrawlerLib
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private const int FirstDepthLevel = 1;
        private readonly int _maxDepthLevel = 6;

        public WebCrawler(int depthLevel)
        {
            if (depthLevel < _maxDepthLevel)
                _maxDepthLevel = depthLevel;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            var result = new CrawlResult(string.Empty)
            {
                NestedResults = await GetNestedCrawlResultsAsync(rootUrls, FirstDepthLevel)
            };
            return result;
        }

        private async Task<List<CrawlResult>> GetNestedCrawlResultsAsync(IEnumerable<string> rootUrls, int depthLevel)
        {
            var nestedCrawlResults = new List<CrawlResult>();
            foreach (var rootUrl in rootUrls)
            {
                nestedCrawlResults.Add(await GetCrawlResultAsync(rootUrl, depthLevel + 1));
            }
            return nestedCrawlResults;
        }

        private async Task<CrawlResult> GetCrawlResultAsync(string rootUrl, int depthLevel)
        {
            var crawlResult = new CrawlResult(rootUrl);
            if (depthLevel <= _maxDepthLevel)
                crawlResult.NestedResults = await GetNestedCrawlResultsAsync(rootUrl, depthLevel);
            return crawlResult;
        }

        private async Task<List<CrawlResult>> GetNestedCrawlResultsAsync(string rootUrl, int depthLevel)
        {
            var nestedUrls = await GetNestedUrlsAsync(rootUrl);
            return await GetNestedCrawlResultsAsync(nestedUrls, depthLevel);
        }

        private static async Task<IEnumerable<string>> GetNestedUrlsAsync(string rootUrl)
        {
            IConfiguration configuration = Configuration.Default.WithDefaultLoader();
            IDocument document = await BrowsingContext.New(configuration).OpenAsync(rootUrl);
            return document.Links.Select(link => link.GetAttribute("href"));
        } 
    }
}

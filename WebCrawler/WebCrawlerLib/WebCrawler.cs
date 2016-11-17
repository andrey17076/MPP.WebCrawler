using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Security.Policy;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using WebCrawlerLib.Contracts;
using WebCrawlerLib.Models;

namespace WebCrawlerLib
{
    public class WebCrawler : ISimpleWebCrawler
    {
        private readonly int _maxDepthLevel = 6;

        public WebCrawler(int depthLevel)
        {
            if (depthLevel < _maxDepthLevel) _maxDepthLevel = depthLevel;
        }

        public async Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls)
        {
            var crawlResult = new CrawlResult();
            foreach (var url in rootUrls)
            {
                crawlResult.NestedLinks.Add(await GetCrawlResultAsync(url, 1));
            }
            return crawlResult;
        }

        private async Task<CrawlResult> GetCrawlResultAsync(string url, int depthLevel)
        {
            var crawlResult = new CrawlResult(url);
            if (depthLevel == _maxDepthLevel) return crawlResult;
            var nestedUrls = await GetNestedUrlsAsync(url);
            foreach (var nestedUrl in nestedUrls)
            {
                crawlResult.NestedLinks.Add(await GetCrawlResultAsync(nestedUrl, depthLevel + 1));
            }
            return crawlResult;
        }

        private static async Task<List<string>> GetNestedUrlsAsync(string url)
        {
            IConfiguration configuration = Configuration.Default.WithDefaultLoader();
            IDocument document = await BrowsingContext.New(configuration).OpenAsync(url);
            return document.Links.Select((link) => link.GetAttribute("href")).ToList();
        }
    }
}

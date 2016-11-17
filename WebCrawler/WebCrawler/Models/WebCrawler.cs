using System.Threading.Tasks;
using WebCrawlerLib.Models;

namespace WebCrawler.Models
{
    internal class WebCrawler
    {
        private readonly WebCrawlerLib.WebCrawler _webCrawler;

        internal WebCrawler(int depth)
        {
            _webCrawler = new WebCrawlerLib.WebCrawler(depth);
        }
    
        internal async Task<CrawlResult> GetCrawlResultAsync(string[] rootUrls)
        {
            return await _webCrawler.PerformCrawlingAsync(rootUrls);
        }
    }
}

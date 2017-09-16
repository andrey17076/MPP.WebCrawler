using System.Threading.Tasks;

namespace WebCrawlerLib
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls);
    }
}

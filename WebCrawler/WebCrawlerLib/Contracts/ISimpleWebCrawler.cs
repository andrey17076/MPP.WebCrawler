using System.Threading.Tasks;
using WebCrawlerLib.Models;

namespace WebCrawlerLib.Contracts
{
    public interface ISimpleWebCrawler
    {
        Task<CrawlResult> PerformCrawlingAsync(string[] rootUrls);
    }
}

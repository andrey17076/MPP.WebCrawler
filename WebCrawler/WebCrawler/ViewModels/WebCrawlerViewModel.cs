using System.Threading.Tasks;
using WebCrawler.Models;
using CrawlResult = WebCrawlerLib.CrawlResult;

namespace WebCrawler.ViewModels
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        private CrawlResult _crawlResult;

        internal WebCrawlerViewModel()
        {
            CrawlCommand = new CrawlCommand(PerformCrawler);
        }

        public CrawlCommand CrawlCommand { get; }

        public CrawlResult CrawlResult
        {
            get
            {
                return _crawlResult;
            }
            set
            {
                if (_crawlResult == value) return;
                _crawlResult = value;
                OnPropertyChanged();
            }
        }

        private async Task PerformCrawler()
        {
            if (!CrawlCommand.CanExecute) return;
            CrawlCommand.CanExecute = false;
            CrawlResult = await GetCrawlResult();
            CrawlCommand.CanExecute = true;
        }

        private static async Task<CrawlResult> GetCrawlResult()
        {
            var config = Config.Load();
            var webCrawler = new WebCrawlerLib.WebCrawler(config.Depth);
            return await webCrawler.PerformCrawlingAsync(config.Urls);
        }
    }
}

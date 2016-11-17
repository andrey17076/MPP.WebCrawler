using WebCrawler.Models;
using CrawlResult = WebCrawlerLib.Models.CrawlResult;

namespace WebCrawler.ViewModels
{
    internal class WebCrawlerViewModel : BaseViewModel
    {
        private CrawlResult _crawlResult;
        private readonly Models.WebCrawler _webCrawler;

        internal WebCrawlerViewModel()
        {
            _webCrawler = new Models.WebCrawler(Config.Load().Depth);
            CrawlCommand = new CrawlCommand(
                async () =>
                {
                    if (CrawlCommand.CanExecute)
                    {
                        CrawlCommand.CanExecute = false;
                        CrawlResult = await _webCrawler.GetCrawlResultAsync(Config.Load().Urls);
                        CrawlCommand.CanExecute = true;
                    }
                }
            );
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
    }
}

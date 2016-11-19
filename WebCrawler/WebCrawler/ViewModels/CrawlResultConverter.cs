using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawlerLib;

namespace WebCrawler.ViewModels
{
    internal class CrawlResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var crawlResult = value as CrawlResult;
            if (value == null)
                return new object();
            return new List<TreeViewItem> {GetTreeViewItem(crawlResult)};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static TreeViewItem GetTreeViewItem(CrawlResult crawlResult)
        {
            var threeViewItem = new TreeViewItem {Header = crawlResult.Link};
            crawlResult.NestedResults.ForEach(result => threeViewItem.Items.Add(GetTreeViewItem(result)));
            return threeViewItem;
        }
    }
}

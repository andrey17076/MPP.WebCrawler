using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using WebCrawlerLib.Models;

namespace WebCrawler.Converters
{
    internal class CrawlResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var crawlResult = value as CrawlResult;
            if (value == null)
                return new object();
            return new List<TreeViewItem>() {GetTreeViewItem(crawlResult)};
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static TreeViewItem GetTreeViewItem(CrawlResult crawlResult)
        {
            var threeViewItem = new TreeViewItem() {Header = crawlResult.Link};
            crawlResult.NestedLinks.ForEach((link) => threeViewItem.Items.Add(GetTreeViewItem(link)));
            return threeViewItem;
        }
    }
}

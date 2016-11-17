using System.Linq;
using System.Xml.Linq;

namespace WebCrawler.Models
{
    internal class Config
    {
        private const string ConfigPath = "config.xml";

        private Config()
        {
        }

        internal int Depth { get; set; }
        internal string[] Urls { get; set; }

        public static Config Load()
        {
            XDocument document = XDocument.Load(ConfigPath);
            XElement root = document.Root;
            return new Config
            {
                Depth = GetDepth(root),
                Urls =  GetUrls(root)
            };
        }

        private static int GetDepth(XContainer root)
        {
            try
            {
                return int.Parse(root.Element("depth").Value);
            }
            catch
            {
                return 0;
            }
        }

        private static string[] GetUrls(XContainer root)
        {
            return root.Element("rootResources")?
                .Elements("resource")
                .Select(x => x.Value)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToArray();
        }
    }
}

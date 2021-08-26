using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YamahaParser.Core.Parser 
{
    class Parser : Loader
    {
        public static string GetCatalogLink() 
        {
            string html = GetMainParts();
            string link = "a";

            HtmlAgilityPack.HtmlDocument document = new();

            if (!string.IsNullOrEmpty(html))
                document.LoadHtml(html);
            link = document.DocumentNode.SelectSingleNode(".//div [@class = 'button parsys']//div [@class = 'row']//div [@class = 'medium-10 large-8 medium-centered columns']//a").Attributes["href"].Value;
            return link;
        }
    }
}

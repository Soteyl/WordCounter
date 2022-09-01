using HtmlAgilityPack;
using NUglify;

namespace WordCounter.Core
{
    public class WebPageAnalizer
    {
        //private const string INNER_TEXT_XPATH = "//*[not(self::script) and not(self::style)]/text()[normalize-space()]";

        private static HtmlWeb web = new HtmlWeb()
        {
            AutoDetectEncoding = true
        };

        private HtmlDocument _pageContent;

        public WebPageAnalizer(string url)
        {
            _pageContent = web.Load(url);
        }

        public string GetCombinedInnerTextFromPage()
        {
            //var nodes = _pageContent.DocumentNode.SelectNodes(INNER_TEXT_XPATH);
            //var text = string.Join("", nodes.Select(t => t.InnerText + " "));
            return Uglify.HtmlToText(_pageContent.Text).Code;
        }
    }
}

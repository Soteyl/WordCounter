using NUglify;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core
{
    public class WebPageAnalyzer: IWebPageAnalyzer
    {
        private readonly IHtmlContentLoader _contentLoader;
        
        public WebPageAnalyzer(IHtmlContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        public string GetCombinedInnerTextFromPage(string url)
        {
            return Uglify.HtmlToText(_contentLoader.Load(url)).Code;
        }
    }
}

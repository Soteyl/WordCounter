using HtmlAgilityPack;
using WordCounter.Core.Interfaces;

namespace WordCounter.Core;

public class HtmlContentLoader: IHtmlContentLoader
{
    private readonly HtmlWeb _web = new()
    {
        AutoDetectEncoding = true
    };
    
    public string Load(string url)
    {
        return _web.Load(url).Text;
    }
}
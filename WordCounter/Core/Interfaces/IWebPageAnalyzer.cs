namespace WordCounter.Core.Interfaces;

public interface IWebPageAnalyzer
{
    string GetCombinedInnerTextFromPage(string url);
}
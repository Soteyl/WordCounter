namespace WordCounter.Core.Interfaces;

public interface IHtmlContentLoader
{
    /// <summary>
    /// Gets html code of page
    /// </summary>
    /// <param name="url">url of the page</param>
    /// <returns></returns>
    string Load(string url);
}
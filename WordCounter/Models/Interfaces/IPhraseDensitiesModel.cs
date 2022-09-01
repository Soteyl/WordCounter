namespace WordCounter.Models.Interfaces;

public interface IPhraseDensitiesModel
{
    /// <summary>
    /// Amount of words in one phrase
    /// </summary>
    public int WordsAmount { get; set; }

    /// <summary>
    /// Amount of phrases to show
    /// </summary>
    public int PhrasesAmount { get; set; }

    /// <summary>
    /// Url of the website to analize for densities
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// If true, majority of articles will not be shown
    /// </summary>
    public bool WithoutArticles { get; set; }
}
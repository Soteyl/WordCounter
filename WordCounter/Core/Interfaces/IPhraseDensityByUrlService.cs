using WordCounter.Models;
using WordCounter.Models.Interfaces;

namespace WordCounter.Core.Interfaces;

public interface IPhraseDensityByUrlService
{
    /// <summary>
    /// Analyzes web page and updates the model
    /// </summary>
    /// <param name="model">Model with url, words and phrases amount</param>
    /// <returns>Densities</returns>
    public IEnumerable<PhraseDensity> AnalyzePage(IPhraseDensitiesModel model);
}
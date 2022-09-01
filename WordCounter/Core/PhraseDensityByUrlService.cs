using WordCounter.Models;

namespace WordCounter.Core
{
    /// <summary>
    /// Service which analizes a web page, handles all the words or phrases, counts their repeats and density.
    /// </summary>
    public class PhraseDensityByUrlService
    {
        private static string? _lastUrl;

        private static PhraseDensityCounter? _densityCounter;

        public PhraseDensitiesModel Initialize()
        {
            return new PhraseDensitiesModel();
        }

        /// <summary>
        /// Aalizes web page and updates the model
        /// </summary>
        /// <param name="model">Model with url, words and phrases amount</param>
        /// <returns>Updated model</returns>
        public PhraseDensitiesModel AnalizePage(PhraseDensitiesModel model)
        {
            if (model.Url != _lastUrl)
            {
                var analizer = new WebPageAnalizer(model.Url);
                string con = analizer.GetCombinedInnerTextFromPage();
                _densityCounter = new PhraseDensityCounter(con);
                _lastUrl = model.Url;
            }
            model.CurrentDensities = GetDensities(model.WordsAmount, model.PhrasesAmount, model.WithoutArticles);
            return model;
        }

        private IEnumerable<PhraseDensity> GetDensities(int wordsAmount, int phrasesAmount, bool withoutArticles)
        {
            IEnumerable<PhraseDensity>? densities;
            densities = _densityCounter!.GetPhrasesDensity(wordsAmount, withoutArticles)
                                                              .OrderByDescending(den => den.RepeatAmount);
            return densities.Take(phrasesAmount);
        }
    }
}

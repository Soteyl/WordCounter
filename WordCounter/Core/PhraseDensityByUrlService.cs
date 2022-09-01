using WordCounter.Core.Interfaces;
using WordCounter.Models;
using WordCounter.Models.Interfaces;

namespace WordCounter.Core
{
    /// <summary>
    /// Service which analyzes a web page, handles all the words or phrases, counts their repeats and density.
    /// </summary>
    public class PhraseDensityByUrlService: IPhraseDensityByUrlService
    {
        private readonly IWebPageAnalyzer _webPageAnalyzer;
        
        public PhraseDensityByUrlService(IWebPageAnalyzer webPageAnalyzer)
        {
            _webPageAnalyzer = webPageAnalyzer;
        }
        
        /// <summary>
        /// Analyzes web page and updates the model
        /// </summary>
        /// <param name="model">Model with url, words and phrases amount</param>
        /// <returns>Densities</returns>
        public IEnumerable<PhraseDensity> AnalyzePage(IPhraseDensitiesModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model is null");
            if (model.Url == null)
                throw new ArgumentNullException("model.Url is null");
            
            string innerText = _webPageAnalyzer.GetCombinedInnerTextFromPage(model.Url);
            var densityCounter = new PhraseDensityCounter(innerText);
            
            IEnumerable<PhraseDensity>? densities = densityCounter
                .GetPhrasesDensity(model.WordsAmount, model.WithoutArticles)
                .OrderByDescending(den => den.RepeatCount);
            
            return densities.Take(model.PhrasesAmount);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WordCounter.Core;
using WordCounter.Models.Interfaces;

namespace WordCounter.Models
{
    /// <summary>
    /// View model for phrase density page.
    /// </summary>
    public class PhraseDensitiesModel: IPhraseDensitiesModel
    {
        /// <summary>
        /// Count of words in one phrase
        /// </summary>
        [BindProperty]
        public int WordsCount { get; set; } = 1;

        /// <summary>
        /// Count of phrases to show
        /// </summary>
        [BindProperty]
        public int PhrasesCount { get; set; } = 4;

        /// <summary>
        /// Url of the website to analize for densities
        /// </summary>
        [BindProperty]
        public string? Url { get; set; }

        /// <summary>
        /// If true, majority of articles will not be shown
        /// </summary>
        [BindProperty]
        public bool WithoutArticles { get; set; }

        /// <summary>
        /// Current densties to show
        /// </summary>
        public IEnumerable<PhraseDensity>? CurrentDensities { get; set; }
    }
}

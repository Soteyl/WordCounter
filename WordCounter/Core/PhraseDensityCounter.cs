using System;
using System.Text.RegularExpressions;
using WordCounter.Models;

namespace WordCounter.Core
{
    public class PhraseDensityCounter
    {
        private static readonly Regex _removeSpecialRegex = new Regex(@"[^0-9a-zA-Z ]+");

        private static readonly Regex _removeMultipleSpacesRegex = new Regex(@"\s+");

        // Articles contains here as an example. It should have it's own configuration in file.
        private static IEnumerable<string> _articles = new List<string>
        {
            "at", "in", "on", "am", "are", "was", "were", "by", "for", 
            "a", "the", "is", "will", "to", "into", "of", "be", "you",
            "i", "he", "she", "they", "me", "your", "my", "his", "her",
            "them", "him", "their", "we", "our", "us", "and", "that", "those"
        };

        private readonly List<string> _splittedWords;

        public readonly string _text;

        private List<string> _splittedWordsWithoutArticles;

        public PhraseDensityCounter(string text)
        {
            _text = _removeSpecialRegex.Replace(text.ToLower(), " ");
            _text = _removeMultipleSpacesRegex.Replace(_text, " ");
            _splittedWords = _text.Split(" ").Where(word => string.IsNullOrWhiteSpace(word) == false).ToList();
        }

        public IEnumerable<PhraseDensity> GetPhrasesDensity(int wordsInPhraseAmount, bool withoutArticles)
        {
            // In original website without articles feature works only while 1 word in phrase
            if (withoutArticles && wordsInPhraseAmount == 1)
            {
                _splittedWordsWithoutArticles ??= _splittedWords.Where(word => _articles.Contains(word) == false).ToList();
                return GetPhraseDensities(_splittedWordsWithoutArticles, wordsInPhraseAmount);
            }
            return GetPhraseDensities(_splittedWords, wordsInPhraseAmount);
        }

        private IEnumerable<PhraseDensity> GetPhraseDensities(List<string> splittedWords, int wordsInPhraseAmount)
        {
            int wordsAmount = splittedWords.Count;
            List<PhraseDensity> densities = new List<PhraseDensity>();

            int stepsAmount = wordsAmount - wordsInPhraseAmount + 1;

            for (int firstPhraseWordIndex = 0;
                firstPhraseWordIndex < stepsAmount;
                firstPhraseWordIndex++)
            {
                var combinedFirst = CombineWords(splittedWords, firstPhraseWordIndex, wordsInPhraseAmount);
                var density = densities.FirstOrDefault(den => den.Phrase.Equals(combinedFirst));
                if (density == null)
                {
                    density = new PhraseDensity()
                    {
                        Phrase = combinedFirst,
                        AllPhrasesInTextAmount = _splittedWords.Count - wordsInPhraseAmount
                    };
                    densities.Add(density);
                }
                density.RepeatAmount++;
            }
            return densities;
        }

        private string CombineWords(List<string> splittedWords, int index, int wordsAmount)
        {
            string result = splittedWords[index];
            for (int i = 1; i < wordsAmount; i++)
            {
                result += " " + splittedWords[index + i];
            }
            return result;
        }
    }
}

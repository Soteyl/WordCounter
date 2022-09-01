using System;
using System.Text;
using System.Text.RegularExpressions;
using WordCounter.Models;

namespace WordCounter.Core
{
    public class PhraseDensityCounter
    {
        private static readonly Regex _removeSpecialRegex = new Regex(@"[^0-9a-zA-Z ]+");

        private static readonly Regex _removeMultipleSpacesRegex = new Regex(@"\s+");

        // Articles contains here as an example. It should have it's own configuration in file.
        private static readonly IEnumerable<string> _articles = new List<string>
        {
            "at", "in", "on", "am", "are", "was", "were", "by", "for", 
            "a", "the", "is", "will", "to", "into", "of", "be", "you",
            "i", "he", "she", "they", "me", "your", "my", "his", "her",
            "them", "him", "their", "we", "our", "us", "and", "that", "those"
        };

        private readonly string[] _splittedWords;

        private string[] _splittedWordsWithoutArticles;

        public PhraseDensityCounter(string text)
        {
            if (text == null)
                throw new ArgumentNullException("text parameter cannot be null.");
            
            text = _removeSpecialRegex.Replace(text.ToLower(), " ");
            text = _removeMultipleSpacesRegex.Replace(text, " ");
            _splittedWords = text.Split(" ").Where(word => string.IsNullOrWhiteSpace(word) == false).ToArray();
            _splittedWordsWithoutArticles = _splittedWords.Where(word => _articles.Contains(word) == false).ToArray();
        }

        public IEnumerable<PhraseDensity> GetPhrasesDensity(int wordsInPhraseCount, bool withoutArticles)
        {
            if (wordsInPhraseCount < 1)
                throw new ArgumentException("Minimum words count: 1");
            
            // In original website without articles feature works only while 1 word in phrase
            if (withoutArticles && wordsInPhraseCount == 1)
            {
                return GetPhraseDensities(_splittedWordsWithoutArticles, wordsInPhraseCount);
            }
            return GetPhraseDensities(_splittedWords, wordsInPhraseCount);
        }

        private IEnumerable<PhraseDensity> GetPhraseDensities(string[] splittedWords, int wordsInPhraseCount)
        {
            int wordsCount = splittedWords.Length;
            List<PhraseDensity> densities = new List<PhraseDensity>();

            int stepsCount = wordsCount - wordsInPhraseCount + 1;

            for (int firstPhraseWordIndex = 0;
                firstPhraseWordIndex < stepsCount;
                firstPhraseWordIndex++)
            {
                var combinedFirst = CombineWords(splittedWords, firstPhraseWordIndex, wordsInPhraseCount);
                var density = densities.FirstOrDefault(den => den.Phrase.Equals(combinedFirst));
                if (density == null)
                {
                    density = new PhraseDensity()
                    {
                        Phrase = combinedFirst,
                        AllPhrasesInTextCount = _splittedWords.Length - wordsInPhraseCount
                    };
                    densities.Add(density);
                }
                density.RepeatCount++;
            }
            return densities;
        }

        private string CombineWords(string[] splittedWords, int index, int wordsCount)
        {
            var sb = new StringBuilder(splittedWords[index]);
            
            for (int i = 1; i < wordsCount; i++)
            {
                sb.Append(' ');
                sb.Append(splittedWords[index + i]);
            }
            return sb.ToString();
        }
    }
}

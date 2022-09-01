namespace WordCounter.Models
{
    public class PhraseDensity
    {
        /// <summary>
        /// Word or several words which density counted
        /// </summary>
        public string? Phrase { get; set; }

        /// <summary>
        /// Count of repeating this phrase in particular text
        /// </summary>
        public int RepeatCount { get; set; }

        /// <summary>
        /// Maximum possible count of phrases in particular text
        /// </summary>
        public int AllPhrasesInTextCount { get; set; }

        /// <summary>
        /// Counts density of some phrase in particular text
        /// </summary>
        /// <returns>percentage of density</returns>
        public double GetDensity() => Math.Round(100.0 / AllPhrasesInTextCount * RepeatCount, 2); 
    }
}

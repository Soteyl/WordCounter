namespace WordCounter.Models
{
    public class PhraseDensity
    {
        /// <summary>
        /// Word or several words which density counted
        /// </summary>
        public string? Phrase { get; set; }

        /// <summary>
        /// Amount of repeating this phrase in particular text
        /// </summary>
        public int RepeatAmount { get; set; }

        /// <summary>
        /// Maximum possible amount of phrases in particular text
        /// </summary>
        public int AllPhrasesInTextAmount { get; set; }

        /// <summary>
        /// Counts density of some phrase in particular text
        /// </summary>
        /// <returns>percentage of density</returns>
        public double GetDensity() => Math.Round(100.0 / AllPhrasesInTextAmount * RepeatAmount, 2); 
    }
}

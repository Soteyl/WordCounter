using Xunit;

using WordCounter.Core;
using WordCounter.Models;

namespace WordCounter.Tests
{
    public class DensityCounterTests
    {
        /// <summary>
        /// Checks densities for specific density words existence.
        /// </summary>
        [Fact]
        public void Returns_Expected_Values()
        {
            // Arrange
            PhraseDensity[] expectedOneWithoutArticles =
            {
                new() { Phrase = "document", RepeatCount = 8 },
                new() { Phrase = "pdf", RepeatCount = 7 },
                new() { Phrase = "pages", RepeatCount = 5 },
                new() { Phrase = "split", RepeatCount = 5 },
                new() { Phrase = "separate", RepeatCount = 4 }
            };
            PhraseDensity[] expectedOneWithArticles =
            {
                new() { Phrase = "you", RepeatCount = 7 },
                new() { Phrase = "a", RepeatCount = 5 },
                new() { Phrase = "the", RepeatCount = 5 },
                new() { Phrase = "of", RepeatCount = 4 },
                new() { Phrase = "will", RepeatCount = 4 }
            };
            PhraseDensity[] expectedTwo =
            {
                new() { Phrase = "pdf document", RepeatCount = 5 },
                new() { Phrase = "split your", RepeatCount = 3 },
                new() { Phrase = "document into", RepeatCount = 3 },
                new() { Phrase = "you can", RepeatCount = 3 },
                new() { Phrase = "split pdf", RepeatCount = 2 }
            };

            string text = "«Split PDF document online is a web service that allows you to split your PDF document into separate\r\npages. This simple application has several modes of operation, you can split your PDF document into\r\nseparate pages, i.e. each page of the original document will be a separate PDF document, you can split\r\nyour document into even and odd pages, this function will come in handy if you need to print a document in\r\nthe form of a book, you can also specify page numbers in the settings and the Split PDF application will\r\ncreate separate PDF documents only with these pages and the fourth mode of operation allows you to\r\ncreate a new PDF document in which there will be only those pages that you specified.»";
            var counter = new PhraseDensityCounter(text);

            // Act
            IEnumerable<PhraseDensity> resultOneWithArticles = counter.GetPhrasesDensity(1, false);
            IEnumerable<PhraseDensity> resultOneWithoutArticles = counter.GetPhrasesDensity(1, true);
            IEnumerable<PhraseDensity> resultTwoWithArticles = counter.GetPhrasesDensity(2, false);
            IEnumerable<PhraseDensity> resultTwoWithoutArticles = counter.GetPhrasesDensity(2, true);

            // Assert
            foreach (PhraseDensity dens in expectedOneWithoutArticles)
            {
                CheckDensity(resultOneWithoutArticles, dens, 126);
                CheckDensity(resultOneWithArticles, dens, 126);
            }
            foreach (PhraseDensity dens in expectedOneWithArticles)
            {
                Assert.DoesNotContain(resultOneWithoutArticles, den => den.Phrase == dens.Phrase);
                CheckDensity(resultOneWithArticles, dens, 126);
            }
            foreach(PhraseDensity dens in expectedTwo)
            {
                CheckDensity(resultTwoWithArticles, dens, 125);
            }
            // Desities for two-word phrases should be equal
            Assert.False(resultTwoWithArticles.Select(dens => dens.Phrase)
                .Except(resultTwoWithoutArticles.Select(dens => dens.Phrase)).Any());
        }
        
        // Checks if densities contains required density
        private void CheckDensity(IEnumerable<PhraseDensity> densities, PhraseDensity density, int words)
        {
            var foundDens = densities.FirstOrDefault(dens => dens.Phrase == density.Phrase);
            Assert.NotNull(foundDens);
            Assert.Equal(words, foundDens.AllPhrasesInTextCount);
            Assert.Equal(density.RepeatCount, foundDens.RepeatCount);
        }
    }
}
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
            // Checks if densities contains required density
            void CheckDensity(IEnumerable<PhraseDensity> densities, PhraseDensity density, int words, bool equal)
            {
                var foundDens = densities.FirstOrDefault(dens => dens.Phrase == density.Phrase);
                if (!equal)
                {
                    Assert.Null(foundDens);
                    return;
                }
                Assert.NotNull(foundDens);
                Assert.Equal(words, foundDens.AllPhrasesInTextAmount);
                Assert.Equal(density.RepeatAmount, foundDens.RepeatAmount);
            }

            PhraseDensity[] expectedOneWithoutArticles =
            {
                new PhraseDensity { Phrase = "document", RepeatAmount = 8 },
                new PhraseDensity { Phrase = "pdf", RepeatAmount = 7 },
                new PhraseDensity { Phrase = "pages", RepeatAmount = 5 },
                new PhraseDensity { Phrase = "split", RepeatAmount = 5 },
                new PhraseDensity { Phrase = "separate", RepeatAmount = 4 }
            };
            PhraseDensity[] expectedOneWithArticles =
            {
                new PhraseDensity { Phrase = "you", RepeatAmount = 7 },
                new PhraseDensity { Phrase = "a", RepeatAmount = 5 },
                new PhraseDensity { Phrase = "the", RepeatAmount = 5 },
                new PhraseDensity { Phrase = "of", RepeatAmount = 4 },
                new PhraseDensity { Phrase = "will", RepeatAmount = 4 }
            };
            PhraseDensity[] expectedTwo =
            {
                new PhraseDensity { Phrase = "pdf document", RepeatAmount = 5 },
                new PhraseDensity { Phrase = "split your", RepeatAmount = 3 },
                new PhraseDensity { Phrase = "document into", RepeatAmount = 3 },
                new PhraseDensity { Phrase = "you can", RepeatAmount = 3 },
                new PhraseDensity { Phrase = "split pdf", RepeatAmount = 2 }
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
                CheckDensity(resultOneWithoutArticles, dens, 126, true);
                CheckDensity(resultOneWithArticles, dens, 126, true);
            }
            foreach (PhraseDensity dens in expectedOneWithArticles)
            {
                CheckDensity(resultOneWithoutArticles, dens, 126, false);
                CheckDensity(resultOneWithArticles, dens, 126, true);
            }
            foreach(PhraseDensity dens in expectedTwo)
            {
                CheckDensity(resultTwoWithArticles, dens, 125, true);
            }
            // Desities for two-word phrases should be equal
            Assert.False(resultTwoWithArticles.Select(dens => dens.Phrase)
                .Except(resultTwoWithoutArticles.Select(dens => dens.Phrase)).Any());
        }
    }
}
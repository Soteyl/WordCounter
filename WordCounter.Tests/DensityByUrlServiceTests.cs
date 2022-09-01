using WordCounter.Core;
using WordCounter.Models;
using Xunit;

namespace WordCounter.Tests;

public class DensityByUrlServiceTests
{
    [Fact]
    public void Returns_Analyzed_Page_With_Articles()
    {
        // Arrange
        var expected = new[]
        {
            new PhraseDensity { Phrase = "the", RepeatCount = 10 },
            new PhraseDensity { Phrase = "of", RepeatCount = 7 },
            new PhraseDensity { Phrase = "w3", RepeatCount = 6 },
            new PhraseDensity { Phrase = "project", RepeatCount = 5 },
        };
        
        var service = new PhraseDensityByUrlService(new WebPageAnalyzer(new HtmlContentLoader()));
        var model = new PhraseDensitiesModel();
        // first website page, will be static long
        model.Url = "http://info.cern.ch/hypertext/WWW/TheProject.html";
        model.WithoutArticles = false;
        model.PhrasesAmount = 4;
        model.WordsAmount = 1;
        
        // Act
        model.CurrentDensities = service.AnalyzePage(model);

        // Assert
        Assert.NotNull(model);
        var densitiesArray = model.CurrentDensities.ToArray();
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(densitiesArray[i].Phrase, expected[i].Phrase);
            Assert.Equal(densitiesArray[i].RepeatCount, expected[i].RepeatCount);
        }
    }
}
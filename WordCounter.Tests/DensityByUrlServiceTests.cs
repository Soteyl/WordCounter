using WordCounter.Core;
using WordCounter.Models;
using Xunit;

namespace WordCounter.Tests;

public class DensityByUrlServiceTests
{
    [Fact]
    public void Kuku()
    {
        // Arrange
        var service = new PhraseDensityByUrlService();

        // Act
        var result = service.Initialize();

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Url);
    }

    [Fact]
    public void Returns_Analized_Page_With_Articles()
    {
        // Arrange
        var expected = new[]
        {
            new PhraseDensity { Phrase = "the", RepeatAmount = 10 },
            new PhraseDensity { Phrase = "of", RepeatAmount = 7 },
            new PhraseDensity { Phrase = "w3", RepeatAmount = 6 },
            new PhraseDensity { Phrase = "project", RepeatAmount = 5 },
        };
        
        var service = new PhraseDensityByUrlService();
        var model = service.Initialize();
        // first website page, will be static long
        model.Url = "http://info.cern.ch/hypertext/WWW/TheProject.html";
        model.WithoutArticles = false;
        model.PhrasesAmount = 4;
        model.WordsAmount = 1;
        
        // Act
        model = service.AnalizePage(model);

        // Assert
        Assert.NotNull(model);
        var densitiesArray = model.CurrentDensities.ToArray();
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(densitiesArray[i].Phrase, expected[i].Phrase);
            Assert.Equal(densitiesArray[i].RepeatAmount, expected[i].RepeatAmount);
        }
    }
}
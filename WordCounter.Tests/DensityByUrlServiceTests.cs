using Moq;
using WordCounter.Core;
using WordCounter.Core.Interfaces;
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
            new PhraseDensity { Phrase = "the", RepeatCount = 11 },
            new PhraseDensity { Phrase = "of", RepeatCount = 7 },
            new PhraseDensity { Phrase = "w3", RepeatCount = 6 },
            new PhraseDensity { Phrase = "project", RepeatCount = 5 },
        };
        var htmlContentMock = new Mock<IHtmlContentLoader>();
        string pageContent = File.ReadAllText("resources/page.html");
        htmlContentMock.Setup(loader => loader.Load(It.IsAny<string>())).Returns(() => pageContent);
        var service = new PhraseDensityByUrlService(new WebPageAnalyzer(htmlContentMock.Object));
        var model = new PhraseDensitiesModel();
        // first website page, will be static long
        model.Url = "some";
        model.WithoutArticles = false;
        model.PhrasesCount = 4;
        model.WordsCount = 1;
        
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
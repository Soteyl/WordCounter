using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WordCounter.Controllers;
using WordCounter.Models;
using Xunit;

namespace WordCounter.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Returns_ActionResult_With_Empty_Model()
    {
        // Arrange
        var home = new HomeController(new NullLogger<HomeController>());

        // Act
        var result = home.Index();

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<PhraseDensitiesModel>(viewResult.Model);
        Assert.Null(model.Url);
    }

    [Fact]
    public void Returns_ActionResult_With_Analized_Page()
    {
        // Arrange
        var home = new HomeController(new NullLogger<HomeController>());
        var model = new PhraseDensitiesModel { Url = "http://info.cern.ch/hypertext/WWW/TheProject.html" };
        // Act
        var result = home.AnalizePage(model);

        // Assert
        Assert.NotNull(result);
        var viewResult = Assert.IsType<ViewResult>(result);
        model = Assert.IsAssignableFrom<PhraseDensitiesModel>(viewResult.Model);
        Assert.NotNull(model.CurrentDensities);
    }
}
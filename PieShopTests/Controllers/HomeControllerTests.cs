using PieShop.Controllers;
using PieShop.ViewModels;
using PieShopTests.Mocks;

namespace PieShopTests.Controllers;

public class HomeControllerTests
{
    [Fact]
    public void Index_Use_PieOfTheWeek_FromRepository()
    {
        // arrange 
        var mockPieRepository = RepositoryMocks.GetPieRepository();
        HomeController homeController = new HomeController(mockPieRepository.Object);

        // act
        var result = homeController.Index().ViewData.Model
            as HomeViewModel;

        // assert
        Assert.NotNull(result);

        var piesOfTheWeek = result?.PiesOfTheWeek?.ToList();
        Assert.NotNull(piesOfTheWeek);
        Assert.True(piesOfTheWeek?.Count() == 3);
        Assert.Equal("Apple Pie", piesOfTheWeek?[0].Name);
    }
}
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;
using PieShopTests.Mocks;

namespace PieShopTests.Controllers;

public class PieControllerTests
{
    private readonly IPieRepository _pieRepository;
    private readonly ICategoryRepository _categoryRepository;

    [Fact]
    public void List_EmptyCategory_ReturnsAllPies()
    {
        // arrange
        var mockPieRepository = RepositoryMocks.GetPieRepository();
        var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

        var pieController = new PieShop.Controllers.PieController(
            mockPieRepository.Object, mockCategoryRepository.Object);
        
        // act
        var result = pieController.List("");
        
        // assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(
            viewResult.ViewData.Model);
        Assert.Equal(10, pieListViewModel.Pies.Count());
    }
}
using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly IShoppingCart _shoppingCart;

    public ShoppingCartController(
        IPieRepository pieRepository, IShoppingCart shoppingCart)
    {
        _pieRepository = pieRepository;
        _shoppingCart = shoppingCart;
    }

    public ViewResult Index()
    {
        var items = _shoppingCart.GetShoppingCardItems();
        _shoppingCart.ShoppingCardItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel(
            _shoppingCart, _shoppingCart.GetShoppingCardTotal());
        
        return View(shoppingCartViewModel);
    }

    public RedirectToActionResult AddToShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.GetPieById(pieId);
        
        if (selectedPie != null)
        {
            _shoppingCart.AddToCard(selectedPie);
        }

        return RedirectToAction("Index");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int pieId)
    {
        var selectedPie = _pieRepository.GetPieById(pieId);
        
        if (selectedPie != null)
        {
            _shoppingCart.RemoveFromCart(selectedPie);
        }

        return RedirectToAction("Index");
    }
}
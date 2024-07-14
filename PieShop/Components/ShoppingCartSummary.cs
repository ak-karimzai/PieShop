using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Components;

public class ShoppingCartSummary : ViewComponent
{
    private readonly IShoppingCart _shoppingCart;

    public ShoppingCartSummary(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCardItems();
        _shoppingCart.ShoppingCardItems = items;

        var shoppingCartViewModel = new ShoppingCartViewModel(
            _shoppingCart, _shoppingCart.GetShoppingCardTotal());

        return View(shoppingCartViewModel);
    }
}
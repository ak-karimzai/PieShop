namespace PieShop.Models;

public interface IShoppingCart
{
    void AddToCard(Pie pie);
    int RemoveFromCart(Pie pie);
    List<ShoppingCardItem> GetShoppingCardItems();
    void ClearCart();
    decimal GetShoppingCardTotal();
    List<ShoppingCardItem> ShoppingCardItems { get; set; }
}
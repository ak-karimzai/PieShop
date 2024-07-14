using Microsoft.EntityFrameworkCore;

namespace PieShop.Models;

public class ShoppingCart : IShoppingCart
{
    private readonly PieShopDbContext _dbContext;
    public string? ShoppingCartId { get; set; }
    public List<ShoppingCardItem> ShoppingCardItems { get; set; } = default!;
    public ShoppingCart(PieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddToCard(Pie pie)
    {
        var shoppingCartItem =
            _dbContext.ShoppingCardItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCardId == ShoppingCartId);
        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCardItem
            {
                ShoppingCardId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };
            _dbContext.ShoppingCardItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }

        _dbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem =
            _dbContext.ShoppingCardItems.SingleOrDefault(
                s => s.Pie.PieId == pie.PieId && s.ShoppingCardId == ShoppingCartId);

        var localAmount = 0;
        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 0)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                _dbContext.ShoppingCardItems.Remove(shoppingCartItem);
            }
        }

        _dbContext.SaveChanges();
        return localAmount;
    }

    public List<ShoppingCardItem> GetShoppingCardItems()
    {
        return ShoppingCardItems ??= _dbContext.ShoppingCardItems
            .Where(cartItem => cartItem.ShoppingCardId == ShoppingCartId)
            .Include(s => s.Pie)
            .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _dbContext.ShoppingCardItems
            .Where(cartItem => cartItem.ShoppingCardId == ShoppingCartId)
            .ToList();
        _dbContext.ShoppingCardItems.RemoveRange(cartItems);
        _dbContext.SaveChanges();
    }

    public decimal GetShoppingCardTotal()
    {
        var total = _dbContext.ShoppingCardItems
            .Where(cartItem => cartItem.ShoppingCardId == ShoppingCartId)
            .Select(c => c.Pie.Price * c.Amount)
            .Sum();
        return total;
    }


    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.
            GetRequiredService<IHttpContextAccessor>()
            ?.HttpContext
            ?.Session;
        PieShopDbContext context = services.GetService<PieShopDbContext>
            () ?? throw new Exception("Error initializing");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
        
        session?.SetString("CartId", cartId);
        return new ShoppingCart(context) { ShoppingCartId = cartId };  
    }
}
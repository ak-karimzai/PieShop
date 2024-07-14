namespace PieShop.Models;

public class OrderRepository : IOrderRepository
{
    private readonly PieShopDbContext _dbContext;
    private readonly IShoppingCart _shoppingCart;

    public OrderRepository(PieShopDbContext dbContext, IShoppingCart shoppingCart)
    {
        _dbContext = dbContext;
        _shoppingCart = shoppingCart;
    }
    
    public void CreateOrder(Order order)
    {
        order.OrderPlaced = DateTime.Now;

        List<ShoppingCardItem>? shoppingCardItems = _shoppingCart.ShoppingCardItems;
        order.OrderTotal = _shoppingCart.GetShoppingCardTotal();
        
        foreach (ShoppingCardItem? shoppingCardItem in shoppingCardItems)
        {
            var orderDetail = new OrderDetail
            {
                Amount = shoppingCardItem.Amount,
                PieId = shoppingCardItem.Pie.PieId,
                Price = shoppingCardItem.Pie.Price
            };
            order.OrderDetails.Add(orderDetail);
        }

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }
}
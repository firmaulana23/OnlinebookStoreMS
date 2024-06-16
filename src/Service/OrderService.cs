using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMS.Data;
using OnlineBookstoreMS.Interface;
using OnlineBookstoreMS.Models.Entity;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;

    public OrderService(ApplicationDbContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    public async Task<Order> PlaceOrder(int userId)
    {
        var cart = await _cartService.GetCartByUserId(userId);
        if (cart == null || !cart.CartItems.Any()) throw new ApplicationException("Cart is empty");

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            TotalPrice = _cartService.GetCartTotalPrice(userId),
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                BookId = ci.BookId,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList()
        };

        _context.Orders.Add(order);

        // Deduct from inventory
        foreach (var item in order.OrderItems)
        {
            var book = await _context.Books.FindAsync(item.BookId);
            if (book == null) throw new ApplicationException("Book not found");
            if (book.Stock < item.Quantity) throw new ApplicationException("Insufficient stock");

            book.Stock -= item.Quantity;
        }

        // Clear the cart
        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Book)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}

using OnlineBookstoreMS.Models.Entity;

namespace OnlineBookstoreMS.Interface
{
    public interface ICartService
    {
        Task<ShoppingCart> GetCartByUserId(int userId);
        Task AddToCart(int userId, int bookId, int quantity);
        Task UpdateCartItem(int userId, int cartItemId, int quantity);
        Task RemoveFromCart(int userId, int cartItemId);
        decimal GetCartTotalPrice(int userId);
    }
}

using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMS.Interface;
using OnlineBookstoreMS.Models.Entity;
using OnlineBookstoreMS.Data;

namespace OnlineBookstoreMS.Services
{

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetCartByUserId(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddToCart(int userId, int bookId, int quantity)
        {
            var cart = await GetCartByUserId(userId);
            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId, CartItems = new List<CartItem>() };
                _context.ShoppingCarts.Add(cart);
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (cartItem == null)
            {
                var book = await _context.Books.FindAsync(bookId);
                if (book == null) throw new ApplicationException("Book not found");

                cartItem = new CartItem
                {
                    ShoppingCartId = cart.Id,
                    BookId = bookId,
                    Quantity = quantity,
                    UnitPrice = book.Price
                };
                cart.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItem(int userId, int cartItemId, int quantity)
        {
            var cart = await GetCartByUserId(userId);
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null) throw new ApplicationException("Cart item not found");

            cartItem.Quantity = quantity;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int userId, int cartItemId)
        {
            var cart = await GetCartByUserId(userId);
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null) throw new ApplicationException("Cart item not found");

            cart.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();
        }

        public decimal GetCartTotalPrice(int userId)
        {
            var cart = GetCartByUserId(userId).Result;
            return cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
        }
    }
}

namespace OnlineBookstoreMS.Models.Entity
{

    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public User User { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

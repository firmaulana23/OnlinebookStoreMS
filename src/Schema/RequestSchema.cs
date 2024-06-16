namespace OnlineBookstoreMS.RequestSchema
{

    public class UserRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class BookRequest
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class CartRequest
    {
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartRequest
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
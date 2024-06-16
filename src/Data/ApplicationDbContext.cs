using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMS.Models.Entity;
namespace OnlineBookstoreMS.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);

        //     modelBuilder.Entity<ShoppingCart>()
        //         .HasOne(sc => sc.User)
        //         .WithMany(u => u.ShoppingCarts)
        //         .HasForeignKey(sc => sc.UserId)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<CartItem>()
        //         .HasOne(ci => ci.ShoppingCart)
        //         .WithMany(sc => sc.CartItems)
        //         .HasForeignKey(ci => ci.ShoppingCartId)
        //         .OnDelete(DeleteBehavior.Cascade);

        //     modelBuilder.Entity<CartItem>()
        //         .HasOne(ci => ci.Book)
        //         .WithMany()
        //         .HasForeignKey(ci => ci.BookId)
        //         .OnDelete(DeleteBehavior.Restrict);
        // }
    }
}

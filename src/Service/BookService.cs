using Microsoft.EntityFrameworkCore;
using OnlineBookstoreMS.Interface;
using OnlineBookstoreMS.Models.Entity;
using OnlineBookstoreMS.Data;

namespace OnlineBookstoreMS.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> GetBookById(int bookId)
        {
            return await _context.Books.FindAsync(bookId);
        }

        public async Task<IEnumerable<Book>> GetBooks(string genre = null, string author = null)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre == genre);
            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author == author);

            return await query.ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
    }

}

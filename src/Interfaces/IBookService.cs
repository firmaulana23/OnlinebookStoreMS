using OnlineBookstoreMS.Models.Entity;

namespace OnlineBookstoreMS.Interface
{
    public interface IBookService
    {
        Task<Book> AddBook(Book book);
        Task<Book> GetBookById(int bookId);
        Task<IEnumerable<Book>> GetBooks(string? genre = null, string? author = null);
        Task UpdateBook(Book book);
        Task DeleteBook(int bookId);
    }

}

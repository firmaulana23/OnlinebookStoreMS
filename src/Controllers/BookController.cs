using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreMS.Models.Entity;
using OnlineBookstoreMS.Interface;
using OnlineBookstoreMS.RequestSchema;
using Microsoft.AspNetCore.Authorization;

namespace OnlineBookstoreMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBook([FromBody] BookRequest req)
        {
            var book = new Book
            {
                Title = req.Title,
                Author = req.Author,
                Genre = req.Genre,
                Price = req.Price,
                Stock = req.Stock
            };

            await _bookService.AddBook(book);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequest req)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            book.Title = req.Title;
            book.Author = req.Author;
            book.Genre = req.Genre;
            book.Price = req.Price;
            book.Stock = req.Stock;

            await _bookService.UpdateBook(book);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] string genre = null, [FromQuery] string author = null)
        {
            var books = await _bookService.GetBooks(genre, author);
            return Ok(books);
        }
    }

}
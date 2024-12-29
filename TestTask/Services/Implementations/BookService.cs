using System.Linq;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Book> GetBook()
        {
            Book book = _context.Books.
                OrderByDescending(b => b.Price * b.QuantityPublished).FirstOrDefault();
            return Task.FromResult(book);
        }
        //public Task<Book> GetBook()
        //{
        //    throw new NotImplementedException();
        //}

        public Task<List<Book>> GetBooks()
        {
            throw new NotImplementedException();
        }
    }
}

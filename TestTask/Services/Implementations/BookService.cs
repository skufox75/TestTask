using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            Book book = _context.Books.OrderByDescending(b => b.Price * b.QuantityPublished).FirstOrDefault();
            return Task.FromResult(book);
        }

        public Task<List<Book>> GetBooks()
        {
            List<Book> books = _context.Books.Where(b => b.Title.Contains("Red") && b.PublishDate >= new DateTime(2012, 5, 25)).ToList<Book>();
            return Task.FromResult(books);
        }
    }
}
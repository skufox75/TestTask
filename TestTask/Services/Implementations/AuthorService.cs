using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Linq;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<Author> GetAuthor()
        {
            int indexOfMajor = _context.Books
            .Select(b => new
            {
                b.AuthorId,
                TitleLength = b.Title.Length
            }).OrderByDescending(a => a.TitleLength).
               ThenBy(a => a.AuthorId).
               First().AuthorId;

            Author result = _context.Authors.Where(a => a.Id == indexOfMajor).FirstOrDefault();
            return Task.FromResult(result);
        }

        public Task<List<Author>> GetAuthors()
        {
            var suitableBooks = _context.Books
                .Where(b => b.PublishDate > new DateTime(2015, 12, 31))
                    .GroupBy(b => b.AuthorId)
                    .Select(g => new
                    {
                        AuthorId = g.Key,
                        BookCount = g.Count()
                    })
                    .Where(g => g.BookCount % 2 == 0);

            List<Author> authors = _context.Authors.Where(a => suitableBooks.Any(sb => sb.AuthorId == a.Id)).ToList();
            return Task.FromResult(authors);
        }
    }
}

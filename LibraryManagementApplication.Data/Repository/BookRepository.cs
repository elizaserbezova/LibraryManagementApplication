using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Data.Repository
{
    public class BookRepository : Repository<Book, int>, IBookRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            this.dbContext = context;
        }

        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            return dbContext.Books.Where(book => book.AuthorId == authorId).ToList();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await dbContext.Books.Where(book => book.GenreId == genreId).ToListAsync();
        }
    }
}

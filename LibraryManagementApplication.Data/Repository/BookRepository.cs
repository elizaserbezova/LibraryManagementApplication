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
        protected readonly ApplicationDbContext dbContext;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            this.dbContext = context;
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await dbContext.Books
                .Where(book => book.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await dbContext.Books
                .Where(book => book.GenreId == genreId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await dbContext.Books
                .Where(book => book.AvailabilityStatus == true)
                .ToListAsync();
        }

        public IQueryable<Book> GetAllAsQuery()
        {
            return dbSet.AsNoTracking();
        }
    }
}

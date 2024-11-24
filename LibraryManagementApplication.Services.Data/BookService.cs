using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels.Books;

namespace LibraryManagementApplication.Services.Data
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext dbContext;

        public BookService(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public async Task AddBookAsync(BookViewModel bookData)
        {
            Book book = new Book()
            {
                Title = bookData.Title,
                AuthorId = bookData.Author,

            }

        }

        public Task DeleteBookAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookViewModel>> GetAllBooksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BookViewModel> GetBookByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(BookViewModel bookDto)
        {
            throw new NotImplementedException();
        }
    }
}

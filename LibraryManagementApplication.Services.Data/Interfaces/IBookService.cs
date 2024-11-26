using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync();
        Book GetBookById(int id);
        Task<BookViewModel?> GetBookByIdAsync(int id);
        void AddBook(Book book);
        Task AddBookAsync(BookViewModel bookViewModel);
        bool UpdateBook(Book book);
        Task<bool> UpdateBookAsync(BookViewModel bookViewModel);
        bool DeleteBook(int id);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookViewModel>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<BookViewModel>> GetBooksByGenreAsync(int genreId);
        Task<IEnumerable<BookViewModel>> GetAvailableBooksAsync();
    }
}

using LibraryManagementApplication.Data.Models;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Book GetBookById(int id);
        Task<Book> GetBookByIdAsync(int id);
        void AddBook(Book book);
        Task AddBookAsync(Book book);
        bool UpdateBook(Book book);
        Task<bool> UpdateBookAsync(Book book);
        bool DeleteBook(int id);
        Task<bool> DeleteBookAsync(int id);
        IEnumerable<Book> GetBooksByAuthor(int authorId);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
    }
}

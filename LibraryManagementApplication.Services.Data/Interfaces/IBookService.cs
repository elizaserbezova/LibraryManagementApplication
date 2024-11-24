using LibraryManagementApplication.ViewModels.Books;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<List<BookViewModel>> GetAllBooksAsync();
        Task<BookViewModel> GetBookByIdAsync(int id);
        Task AddBookAsync(BookViewModel bookDto);
        Task UpdateBookAsync(BookViewModel bookDto);
        Task DeleteBookAsync(int id);
    }
}

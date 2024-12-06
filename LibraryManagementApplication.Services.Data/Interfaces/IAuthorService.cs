
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);
    }
}

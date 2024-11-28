
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync();
        Task<AuthorViewModel?> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(AuthorViewModel viewModel);
        Task<bool> UpdateAuthorAsync(AuthorViewModel viewModel);
        Task<bool> DeleteAuthorAsync(int id);
    }
}

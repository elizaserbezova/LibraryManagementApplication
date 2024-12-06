using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();

        Task<Genre?> GetGenreByIdAsync(int id);

        Task AddGenreAsync(Genre genre);

        Task<bool> UpdateGenreAsync(Genre genre);

        Task<bool> DeleteGenreAsync(int id);
    }
}

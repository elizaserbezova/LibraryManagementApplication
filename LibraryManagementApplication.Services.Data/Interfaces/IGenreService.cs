using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllGenresAsync();

        Task<GenreViewModel?> GetGenreByIdAsync(int id);

        Task AddGenreAsync(GenreViewModel genreViewModel);

        Task<bool> UpdateGenreAsync(GenreViewModel genreViewModel);

        Task<bool> DeleteGenreAsync(int id);
    }
}

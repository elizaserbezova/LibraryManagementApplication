using AutoMapper;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data
{
    public class GenreService : IGenreService
    {
        IGenreRepository repository;

        public GenreService(IGenreRepository _repository)
        {
            repository = _repository;
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await repository.AddAsync(genre);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var genres = await repository.GetAllAsync();
            return genres;
        }

        public async Task<Genre?> GetGenreByIdAsync(int id)
        {
            var genre = await repository.GetByIdAsync(id);
            return genre;
        }

        public async Task<bool> UpdateGenreAsync(Genre genre)
        {
            return await repository.UpdateAsync(genre);
        }
    }
}

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
        IMapper mapper;

        public GenreService(IGenreRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        public async Task AddGenreAsync(GenreViewModel genreViewModel)
        {
            var genre = mapper.Map<Genre>(genreViewModel);
            await repository.AddAsync(genre);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            return await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GenreViewModel>> GetAllGenresAsync()
        {
            var genres = await repository.GetAllAsync();
            return mapper.Map<IEnumerable<GenreViewModel>>(genres);
        }

        public async Task<GenreViewModel?> GetGenreByIdAsync(int id)
        {
            var genre = await repository.GetByIdAsync(id);
            return mapper.Map<GenreViewModel>(genre);
        }

        public async Task<bool> UpdateGenreAsync(GenreViewModel genreViewModel)
        {
            var genre = mapper.Map<Genre>(genreViewModel);
            return await repository.UpdateAsync(genre);
        }
    }
}

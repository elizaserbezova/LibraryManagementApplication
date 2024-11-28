using AutoMapper;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;

namespace LibraryManagementApplication.Services.Data
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task AddAuthorAsync(AuthorViewModel viewModel)
        {
            var author = _mapper.Map<Author>(viewModel);
            await _authorRepository.AddAsync(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
        }

        public async Task<AuthorViewModel?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return _mapper.Map<AuthorViewModel>(author);
        }

        public async Task<bool> UpdateAuthorAsync(AuthorViewModel viewModel)
        {
            var author = _mapper.Map<Author>(viewModel);
            return await _authorRepository.UpdateAsync(author);
        }
    }
}

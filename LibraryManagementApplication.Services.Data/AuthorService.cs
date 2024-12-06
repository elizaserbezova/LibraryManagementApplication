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

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return authors;
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return author;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            return await _authorRepository.UpdateAsync(author);
        }
    }
}

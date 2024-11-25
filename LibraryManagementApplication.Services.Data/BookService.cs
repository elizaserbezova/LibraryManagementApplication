using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;

namespace LibraryManagementApplication.Services.Data
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository repository)
        {
            _bookRepository = repository;
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public bool UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            return await _bookRepository.UpdateAsync(book);
        }

        public bool DeleteBook(int id)
        {
            return _bookRepository.Delete(id);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }

        public IEnumerable<Book> GetBooksByAuthor(int authorId)
        {
            return _bookRepository.GetBooksByAuthor(authorId);
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _bookRepository.GetBooksByGenreAsync(genreId);
        }
    }
}

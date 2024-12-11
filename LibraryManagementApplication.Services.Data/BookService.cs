using AutoMapper;
using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagementApplication.Services.Data
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILendingRecordRepository _lendingRecordRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository repository, IMapper mapper, ILendingRecordRepository lendingRecordRepository)
        {
            _bookRepository = repository;
            _mapper = mapper;
            _lendingRecordRepository = lendingRecordRepository;
        }

        public void AddBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public async Task AddBookAsync(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            await _bookRepository.AddAsync(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync(string? search = null)
        {
            var query = _bookRepository
                .GetAllAsQuery()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(b => b.Title.Contains(search));
            }

            var lentOutBookIds = await _lendingRecordRepository
                .GetAllAsQuery()
                .Where(r => r.ReturnDate == null)
                .Select(r => r.BookId)
                .Distinct()
                .ToListAsync();

            var books = await query.ToListAsync();

            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);

            foreach (var bookViewModel in bookViewModels)
            {
                bookViewModel.AvailabilityStatus = !lentOutBookIds.Contains(bookViewModel.BookId);
            }

            return bookViewModels;
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository
                .GetAllAsQuery()
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);

            return _mapper.Map<BookViewModel>(book);
        }

        public bool UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }

        public async Task<bool> UpdateBookAsync(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
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

        public async Task<IEnumerable<BookViewModel>> GetBooksByAuthorAsync(int authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }

        public async Task<IEnumerable<BookViewModel>> GetBooksByGenreAsync(int genreId)
        {
            var books = await _bookRepository.GetBooksByGenreAsync(genreId);
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }

        public async Task<IEnumerable<BookViewModel>> GetAvailableBooksAsync()
        {
            var books = await _bookRepository.GetAvailableBooksAsync();
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }
    }
}

using AutoMapper;
using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data;
using LibraryManagementApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace LibraryManagementApplication.Tests.Services
{
    [TestFixture]
    public class BookServiceTests
    {
        private IMapper _mapper;
        private DbContextOptions<ApplicationDbContext> _dbOptions;
        private ApplicationDbContext _context;
        private BookRepository _bookRepository;
        private Mock<ILendingRecordRepository> _mockLendingRecordRepository;
        private BookService _bookService;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagementTestDb_Authors")
                .Options;
            _context = new ApplicationDbContext(_dbOptions);
            _context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();

            _bookRepository = new BookRepository(_context);
            _mockLendingRecordRepository = new Mock<ILendingRecordRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = new Mapper(mapperConfig);

            _bookService = new BookService(_bookRepository, _mapper, _mockLendingRecordRepository.Object);

            SeedDatabase();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void SeedDatabase()
        {
            _context.Authors.RemoveRange(_context.Authors);
            _context.Genres.RemoveRange(_context.Genres);
            _context.Books.RemoveRange(_context.Books);
            _context.SaveChanges();

            _context.Authors.Add(new Author { AuthorId = 1, Name = "Author 1" });
            _context.Genres.Add(new Genre { GenreId = 1, Name = "Genre 1" });
            _context.Books.AddRange(new List<Book>
            {
                new Book { BookId = 1, Title = "Book 1", ISBN="123123123", AuthorId = 1, GenreId = 1, AvailabilityStatus = true },
                new Book { BookId = 2, Title = "Book 2", ISBN="1231231231", AuthorId = 1, GenreId = 1, AvailabilityStatus = true }
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task AddBookAsync_ShouldAddBookSuccessfully()
        {
            var bookViewModel = new BookViewModel
            {
                Title = "New Book",
                ISBN = "1234567890",
                AuthorId = 1,
                GenreId = 1,
                PublishDate = System.DateTime.Now,
                AvailabilityStatus = true
            };

            await _bookService.AddBookAsync(bookViewModel);

            var book = _context.Books.FirstOrDefault(b => b.Title == "New Book");
            Assert.IsNotNull(book);
            Assert.AreEqual("New Book", book.Title);
        }

        //[Test]
        //public async Task GetAllBooksAsync_ShouldReturnAllBooks()
        //{
        //    var books = await _bookService.GetAllBooksAsync();

        //    Assert.AreEqual(2, books.Count());
        //    Assert.IsTrue(books.Any(b => b.Title == "Book 1"));
        //    Assert.IsTrue(books.Any(b => b.Title == "Book 2"));
        //}

        [Test]
        public async Task GetBookByIdAsync_ShouldReturnCorrectBook()
        {
            var book = await _bookService.GetBookByIdAsync(1);

            Assert.IsNotNull(book);
            Assert.AreEqual("Book 1", book.Title);
        }

        //[Test]
        //public async Task UpdateBookAsync_ShouldUpdateBookSuccessfully()
        //{
        //    var bookViewModel = new BookViewModel
        //    {
        //        BookId = 1,
        //        Title = "Updated Book",
        //        ISBN = "1234567890",
        //        AuthorId = 1,
        //        GenreId = 1
        //    };

        //    var result = await _bookService.UpdateBookAsync(bookViewModel);

        //    Assert.IsTrue(result);
        //    var updatedBook = _context.Books.FirstOrDefault(b => b.BookId == 1);
        //    Assert.AreEqual("Updated Book", updatedBook.Title);
        //}

        [Test]
        public async Task DeleteBookAsync_ShouldDeleteBookSuccessfully()
        {
            var result = await _bookService.DeleteBookAsync(1);

            Assert.IsTrue(result);
            var deletedBook = _context.Books.FirstOrDefault(b => b.BookId == 1);
            Assert.IsNull(deletedBook);
        }

        [Test]
        public async Task GetBooksByAuthorAsync_ShouldReturnAuthorsBooks()
        {
            var books = await _bookService.GetBooksByAuthorAsync(1);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.All(b => b.AuthorId == 1));
        }

        [Test]
        public async Task GetBooksByGenreAsync_ShouldReturnGenresBooks()
        {
            var books = await _bookService.GetBooksByGenreAsync(1);

            Assert.AreEqual(2, books.Count());
            Assert.IsTrue(books.All(b => b.GenreId == 1));
        }

        [Test]
        public async Task GetAvailableBooksAsync_ShouldReturnOnlyAvailableBooks()
        {
            _context.Books.First(b => b.BookId == 1).AvailabilityStatus = false;
            _context.SaveChanges();

            var books = await _bookService.GetAvailableBooksAsync();

            Assert.AreEqual(1, books.Count());
            Assert.IsTrue(books.All(b => b.AvailabilityStatus));
        }
    }
}

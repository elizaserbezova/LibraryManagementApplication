using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Services.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;


namespace LibraryManagementApplication.Tests.Services
{
    [TestFixture]
    public class AuthorServiceTests
    {
        private DbContextOptions<ApplicationDbContext> _dbOptions;
        private ApplicationDbContext _context;
        private AuthorRepository _authorRepository;
        private AuthorService _authorService;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagementTestDb_Authors")
                .Options;
            _context = new ApplicationDbContext(_dbOptions);
            _context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();

            _authorRepository = new AuthorRepository(_context);
            _authorService = new AuthorService(_authorRepository);

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
            _context.SaveChanges();

            _context.Authors.AddRange(new List<Author>
            {
                new Author { AuthorId = 1, Name = "Author 1" },
                new Author { AuthorId = 2, Name = "Author 2" }
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task AddAuthorAsync_ShouldAddAuthorSuccessfully()
        {
            var newAuthor = new Author { AuthorId = 3, Name = "Author 3" };

            await _authorService.AddAuthorAsync(newAuthor);

            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == 3);
            Assert.IsNotNull(author);
            Assert.AreEqual("Author 3", author.Name);
        }

        [Test]
        public async Task GetAllAuthorsAsync_ShouldReturnAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            Assert.AreEqual(2, authors.Count());
            Assert.IsTrue(authors.Any(a => a.Name == "Author 1"));
            Assert.IsTrue(authors.Any(a => a.Name == "Author 2"));
        }

        [Test]
        public async Task GetAuthorByIdAsync_ShouldReturnCorrectAuthor()
        {
            var author = await _authorService.GetAuthorByIdAsync(1);

            Assert.IsNotNull(author);
            Assert.AreEqual("Author 1", author.Name);
        }

        //[Test]
        //public async Task UpdateAuthorAsync_ShouldUpdateAuthorSuccessfully()
        //{
        //    var updatedAuthor = new Author { AuthorId = 1, Name = "Updated Author" };

        //    var result = await _authorService.UpdateAuthorAsync(updatedAuthor);

        //    Assert.IsTrue(result);
        //    var author = _context.Authors.FirstOrDefault(a => a.AuthorId == 1);
        //    Assert.AreEqual("Updated Author", author.Name);
        //}

        [Test]
        public async Task DeleteAuthorAsync_ShouldDeleteAuthorSuccessfully()
        {
            var result = await _authorService.DeleteAuthorAsync(1);

            Assert.IsTrue(result);
            var author = _context.Authors.FirstOrDefault(a => a.AuthorId == 1);
            Assert.IsNull(author);
        }

        [Test]
        public async Task DeleteAuthorAsync_ShouldReturnFalseWhenAuthorDoesNotExist()
        {
            var result = await _authorService.DeleteAuthorAsync(999);

            Assert.IsFalse(result);
        }
    }
}

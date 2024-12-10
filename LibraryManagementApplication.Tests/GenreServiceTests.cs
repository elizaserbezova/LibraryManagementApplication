using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Services.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LibraryManagementApplication.Tests.Services
{
    [TestFixture]
    public class GenreServiceTests
    {
        private DbContextOptions<ApplicationDbContext> _dbOptions;
        private ApplicationDbContext _context;
        private GenreRepository _genreRepository;
        private GenreService _genreService;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagementTestDb_Genres")
                .Options;
            _context = new ApplicationDbContext(_dbOptions);
            _context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();

            _genreRepository = new GenreRepository(_context);
            _genreService = new GenreService(_genreRepository);

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
            _context.Genres.RemoveRange(_context.Genres);
            _context.SaveChanges();

            _context.Genres.AddRange(new List<Genre>
            {
                new Genre { GenreId = 1, Name = "Fiction" },
                new Genre { GenreId = 2, Name = "Non-Fiction" }
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task AddGenreAsync_ShouldAddGenreSuccessfully()
        {
            var newGenre = new Genre { GenreId = 3, Name = "Science Fiction" };

            await _genreService.AddGenreAsync(newGenre);

            var genre = _context.Genres.FirstOrDefault(g => g.GenreId == 3);
            Assert.IsNotNull(genre);
            Assert.AreEqual("Science Fiction", genre.Name);
        }

        [Test]
        public async Task GetAllGenresAsync_ShouldReturnAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();

            Assert.AreEqual(2, genres.Count());
            Assert.IsTrue(genres.Any(g => g.Name == "Fiction"));
            Assert.IsTrue(genres.Any(g => g.Name == "Non-Fiction"));
        }

        [Test]
        public async Task GetGenreByIdAsync_ShouldReturnCorrectGenre()
        {
            var genre = await _genreService.GetGenreByIdAsync(1);

            Assert.IsNotNull(genre);
            Assert.AreEqual("Fiction", genre.Name);
        }

        //[Test]
        //public async Task UpdateGenreAsync_ShouldUpdateGenreSuccessfully()
        //{
        //    var updatedGenre = new Genre { GenreId = 1, Name = "Updated Fiction" };

        //    var result = await _genreService.UpdateGenreAsync(updatedGenre);

        //    Assert.IsTrue(result);
        //    var genre = _context.Genres.FirstOrDefault(g => g.GenreId == 1);
        //    Assert.AreEqual("Updated Fiction", genre.Name);
        //}

        [Test]
        public async Task DeleteGenreAsync_ShouldDeleteGenreSuccessfully()
        {
            var result = await _genreService.DeleteGenreAsync(1);

            Assert.IsTrue(result);
            var genre = _context.Genres.FirstOrDefault(g => g.GenreId == 1);
            Assert.IsNull(genre);
        }

        [Test]
        public async Task DeleteGenreAsync_ShouldReturnFalseWhenGenreDoesNotExist()
        {
            var result = await _genreService.DeleteGenreAsync(999);

            Assert.IsFalse(result);
        }
    }
}

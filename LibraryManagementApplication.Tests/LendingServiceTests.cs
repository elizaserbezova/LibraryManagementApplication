using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Services.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Tests.Services
{
    [TestFixture]
    public class LendingServiceTests
    {
        private DbContextOptions<ApplicationDbContext> _dbOptions;
        private ApplicationDbContext _context;
        private LendingRecordRepository _lendingRecordRepository;
        private BookRepository _bookRepository;
        private LendingService _lendingService;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagementTestDb_Lending")
                .Options;

            _context = new ApplicationDbContext(_dbOptions);
            _context.Database.EnsureCreated();

            _lendingRecordRepository = new LendingRecordRepository(_context);
            _bookRepository = new BookRepository(_context);
            _lendingService = new LendingService(_lendingRecordRepository, _bookRepository);

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
            _context.LendingRecords.RemoveRange(_context.LendingRecords);
            _context.Books.RemoveRange(_context.Books);
            _context.SaveChanges();

            _context.Books.Add(new Book { BookId = 1, ISBN = "1231231231", Title = "Test Book", AvailabilityStatus = true });
            _context.Books.Add(new Book { BookId = 2, ISBN="123123123", Title = "Lent Book", AvailabilityStatus = true });

            _context.LendingRecords.Add(new LendingRecord
            {
                LendingRecordId = 1,
                BookId = 2,
                MemberId = 1,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            });

            _context.SaveChanges();
        }

        [Test]
        public async Task GetMemberLentRecordsAsync_ShouldReturnLentBooksForMember()
        {
            var records = await _lendingService.GetMemberLentRecordsAsync(1);

            Assert.AreEqual(1, records.Count());
            Assert.AreEqual(2, records.First().BookId);
        }

        [Test]
        public async Task IsBookLentOutAsync_ShouldReturnTrueIfBookIsLent()
        {
            var result = await _lendingService.IsBookLentOutAsync(2);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsBookLentOutAsync_ShouldReturnFalseIfBookIsNotLent()
        {
            var result = await _lendingService.IsBookLentOutAsync(1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task LendBookAsync_ShouldLendBookSuccessfully()
        {
            var result = await _lendingService.LendBookAsync(1, 2);

            Assert.IsTrue(result);

            var record = _context.LendingRecords.FirstOrDefault(r => r.BookId == 1);
            Assert.IsNotNull(record);
            Assert.AreEqual(2, record.MemberId);
        }

        [Test]
        public async Task LendBookAsync_ShouldReturnFalseIfBookAlreadyLent()
        {
            var result = await _lendingService.LendBookAsync(2, 3);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReturnBookAsync_ShouldReturnBookSuccessfully()
        {
            var result = await _lendingService.ReturnBookAsync(1, 1);

            Assert.IsTrue(result);

            var record = _context.LendingRecords.FirstOrDefault(r => r.LendingRecordId == 1);
            Assert.IsNotNull(record);
            Assert.IsNotNull(record.ReturnDate);
        }

        [Test]
        public async Task ReturnBookAsync_ShouldReturnFalseIfLendingRecordNotFound()
        {
            var result = await _lendingService.ReturnBookAsync(999, 1);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReturnBookAsync_ShouldReturnFalseIfMemberMismatch()
        {
            var result = await _lendingService.ReturnBookAsync(1, 999);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReturnBookAsync_ShouldReturnFalseIfBookAlreadyReturned()
        {
            var record = _context.LendingRecords.FirstOrDefault(r => r.LendingRecordId == 1);
            record.ReturnDate = DateTime.Now;
            _context.SaveChanges();

            var result = await _lendingService.ReturnBookAsync(1, 1);

            Assert.IsFalse(result);
        }
    }
}

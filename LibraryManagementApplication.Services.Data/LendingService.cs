using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Services.Data
{
    public class LendingService : ILendingService
    {
        private readonly ILendingRecordRepository _lendingRecordRepository;
        private readonly IBookRepository _bookRepository;

        public LendingService(ILendingRecordRepository lendingRecordRepository, IBookRepository bookRepository)
        {
            _lendingRecordRepository = lendingRecordRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<LendingRecord>> GetMemberLentRecordsAsync(int memberId)
        {
            return await _lendingRecordRepository
                .GetAllAsQuery()
                .Where(r => r.MemberId == memberId && r.ReturnDate == null)
                .Include(r => r.Book)
                .ToListAsync();
        }

        public async Task<bool> IsBookLentOutAsync(int bookId)
        {
            var allRecords = await _lendingRecordRepository.GetAllAsync();
            return allRecords
                .Any(r => r.BookId == bookId && r.ReturnDate == null);
        }

        public async Task<bool> LendBookAsync(int bookId, int memberId)
        {
            if (await IsBookLentOutAsync(bookId))
            {
                return false;
            }

            var record = new LendingRecord
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.Now,
                ReturnDate = null
            };

            await _lendingRecordRepository.AddAsync(record);
            return true;
        }

        public async Task<bool> ReturnBookAsync(int lendingRecordId, int memberId)
        {
            var record = await _lendingRecordRepository.GetByIdAsync(lendingRecordId);

            if (record == null || record.MemberId != memberId || record.ReturnDate != null)
            {
                return false;
            }

            record.ReturnDate = DateTime.Now;
            return await _lendingRecordRepository.UpdateAsync(record);
        }
    }
}

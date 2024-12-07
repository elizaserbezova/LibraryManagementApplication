using LibraryManagementApplication.Data.Models;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface ILendingService
    {
        Task<bool> LendBookAsync(int bookId, int memberId);
        Task<bool> ReturnBookAsync(int lendingRecordId, int memberId);
        Task<IEnumerable<LendingRecord>> GetMemberLentRecordsAsync(int memberId);
        Task<bool> IsBookLentOutAsync(int bookId);
    }
}

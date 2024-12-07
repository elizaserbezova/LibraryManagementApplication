using LibraryManagementApplication.Data.Models;

namespace LibraryManagementApplication.Data.Repository.Interfaces
{
    public interface ILendingRecordRepository : IRepository<LendingRecord, int>
    {
        IQueryable<LendingRecord> GetAllAsQuery();
    }
}

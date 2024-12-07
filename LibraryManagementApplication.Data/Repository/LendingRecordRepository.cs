using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;

namespace LibraryManagementApplication.Data.Repository
{
    public class LendingRecordRepository : Repository<LendingRecord, int>, ILendingRecordRepository
    {
        public LendingRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

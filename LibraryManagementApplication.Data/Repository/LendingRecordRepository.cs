using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Repository
{
    public class LendingRecordRepository : Repository<LendingRecord, int>, ILendingRecordRepository
    {
        public LendingRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<LendingRecord> GetAllAsQuery()
        {
            return dbSet.AsNoTracking();
        }
    }
}

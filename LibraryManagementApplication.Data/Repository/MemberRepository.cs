using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApplication.Data.Repository
{
    public class MemberRepository : Repository<Member, int>, IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Member? GetMemberByUserId(string userId)
        {
            return _dbContext.Members.FirstOrDefault(m => m.UserId == userId);
        }

        public async Task<Member?> GetMemberByUserIdAsync(string userId)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(m => m.UserId == userId);
        }
    }
}

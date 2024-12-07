using LibraryManagementApplication.Data.Models;

namespace LibraryManagementApplication.Data.Repository.Interfaces
{
    public interface IMemberRepository : IRepository<Member, int>
    {
        Member? GetMemberByUserId(string userId);
        Task<Member?> GetMemberByUserIdAsync(string userId);
    }
}

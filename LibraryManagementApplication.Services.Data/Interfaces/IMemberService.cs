using LibraryManagementApplication.Data.Models;

namespace LibraryManagementApplication.Services.Data.Interfaces
{
    public interface IMemberService
    {
        Task<Member?> GetMemberByIdAsync(int memberId);
        Task<Member?> GetMemberByUserIdAsync(string userId);
        Task<IEnumerable<Member>> GetAllMembersAsync();
        Task CreateMemberAsync(Member member);
        Task<bool> UpdateMemberAsync(Member member);
        Task<bool> DeleteMemberAsync(int memberId);
    }
}

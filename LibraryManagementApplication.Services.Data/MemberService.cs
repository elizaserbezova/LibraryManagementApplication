using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository.Interfaces;
using LibraryManagementApplication.Services.Data.Interfaces;

namespace LibraryManagementApplication.Services.Data
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task CreateMemberAsync(Member member)
        {
            await _memberRepository.AddAsync(member);
        }

        public async Task<bool> DeleteMemberAsync(int memberId)
        {
            return await _memberRepository.DeleteAsync(memberId);
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int memberId)
        {
            return await _memberRepository.GetByIdAsync(memberId);
        }

        public async Task<Member?> GetMemberByUserIdAsync(string userId)
        {
            return await _memberRepository.GetMemberByUserIdAsync(userId);
        }

        public async Task<bool> UpdateMemberAsync(Member member)
        {
            return await _memberRepository.UpdateAsync(member);
        }
    }
}

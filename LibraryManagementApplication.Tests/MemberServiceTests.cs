using LibraryManagementApplication.Data;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Data.Repository;
using LibraryManagementApplication.Services.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementApplication.Tests.Services
{
    [TestFixture]
    public class MemberServiceTests
    {
        private DbContextOptions<ApplicationDbContext> _dbOptions;
        private ApplicationDbContext _context;
        private MemberRepository _memberRepository;
        private MemberService _memberService;

        [SetUp]
        public void Setup()
        {
            _dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagementTestDb_Members")
                .Options;
            _context = new ApplicationDbContext(_dbOptions);
            _context.Database.EnsureCreated();
            _context.Database.EnsureDeleted();

            _memberRepository = new MemberRepository(_context);
            _memberService = new MemberService(_memberRepository);

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
            _context.Members.RemoveRange(_context.Members);
            _context.SaveChanges();

            _context.Members.AddRange(new List<Member>
            {
                new Member { MemberId = 1, Name = "Member 1", Email = "member1@example.com", UserId = "User1", ContactInfo = "Info 1"},
                new Member { MemberId = 2, Name = "Member 2", Email = "member2@example.com", UserId = "User2", ContactInfo = "Info 2" }
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllMembersAsync_ShouldReturnAllMembers()
        {
            var members = await _memberService.GetAllMembersAsync();

            Assert.AreEqual(2, members.Count());
            Assert.IsTrue(members.Any(m => m.Name == "Member 1"));
            Assert.IsTrue(members.Any(m => m.Name == "Member 2"));
        }

        [Test]
        public async Task GetMemberByIdAsync_ShouldReturnCorrectMember()
        {
            var member = await _memberService.GetMemberByIdAsync(1);

            Assert.IsNotNull(member);
            Assert.AreEqual("Member 1", member.Name);
        }

        [Test]
        public async Task GetMemberByUserIdAsync_ShouldReturnCorrectMember()
        {
            var member = await _memberService.GetMemberByUserIdAsync("User1");

            Assert.IsNotNull(member);
            Assert.AreEqual("Member 1", member.Name);
        }

        [Test]
        public async Task CreateMemberAsync_ShouldAddMemberSuccessfully()
        {
            var newMember = new Member { MemberId = 3, Name = "Member 3", Email = "member3@example.com", UserId = "User3", ContactInfo = "Info 3" };

            await _memberService.CreateMemberAsync(newMember);

            var member = _context.Members.FirstOrDefault(m => m.MemberId == 3);
            Assert.IsNotNull(member);
            Assert.AreEqual("Member 3", member.Name);
        }

        [Test]
        public async Task DeleteMemberAsync_ShouldDeleteMemberSuccessfully()
        {
            var result = await _memberService.DeleteMemberAsync(1);

            Assert.IsTrue(result);
            var member = _context.Members.FirstOrDefault(m => m.MemberId == 1);
            Assert.IsNull(member);
        }

        [Test]
        public async Task DeleteMemberAsync_ShouldReturnFalseForNonExistentMember()
        {
            var result = await _memberService.DeleteMemberAsync(99);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateMemberAsync_ShouldUpdateMemberSuccessfully()
        {
            var member = await _memberService.GetMemberByIdAsync(1);
            member.Name = "Updated Member 1";

            var result = await _memberService.UpdateMemberAsync(member);

            Assert.IsTrue(result);
            var updatedMember = _context.Members.FirstOrDefault(m => m.MemberId == 1);
            Assert.AreEqual("Updated Member 1", updatedMember.Name);
        }
    }
}

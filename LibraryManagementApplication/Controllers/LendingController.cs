using AutoMapper;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    [Authorize]
    public class LendingController : Controller
    {
        private readonly ILendingService _lendingService;
        private readonly IMemberService _memberService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public LendingController(ILendingService lendingService, IMemberService memberService, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _lendingService = lendingService;
            _memberService = memberService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var member = await _memberService.GetMemberByUserIdAsync(user.Id);

            if (member == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var lendingRecords = await _lendingService.GetMemberLentRecordsAsync(member.MemberId);

            var lentBooks = _mapper.Map<IEnumerable<LentBookViewModel>>(lendingRecords);

            return View(lentBooks);
        }

        [HttpPost]
        public async Task<IActionResult> Return(int lendingRecordId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var member = await _memberService.GetMemberByUserIdAsync(user.Id);
            if (member == null)
            {
                return View("Error", "Member record not found for the logged-in user.");
            }

            await _lendingService.ReturnBookAsync(lendingRecordId, member.MemberId);
            return RedirectToAction(nameof(Index));
        }
    }
}

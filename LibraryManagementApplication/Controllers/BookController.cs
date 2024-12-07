using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Data;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly ILendingService _lendingService;
        private readonly IMemberService _memberService;
        private readonly UserManager<IdentityUser> _userManager;

        public BookController(
            IBookService bookService, 
            IAuthorService authorService, 
            IGenreService genreService,
            ILendingService lendingService,
            IMemberService memberService,
            UserManager<IdentityUser> userManager)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _lendingService = lendingService;
            _memberService = memberService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    // User not logged in - redirect to login
                    return RedirectToPage("/Account/Login");
                }

                var member = await _memberService.GetMemberByUserIdAsync(user.Id);
                if (member == null)
                {
                    return View("Error", "Member record not found for the logged-in user.");
                }

                var lendingRecords = await _lendingService.GetMemberLentRecordsAsync(member.MemberId);
                var userLentBookIds = lendingRecords.Select(r => r.BookId).Distinct().ToList();

                foreach (var book in books)
                {
                    var record = lendingRecords
                        .FirstOrDefault(r => r.BookId == book.BookId && r.ReturnDate == null);

                    if (record != null)
                    {
                        book.IsLentByUser = true;
                        book.LendingRecordId = record.LendingRecordId;
                    }
                }
            }

            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            var genres = await _genreService.GetAllGenresAsync();

            var viewModel = new BookViewModel
            {
                AvailabilityStatus = true,
                Authors = authors.ToList(),
                Genres = genres.ToList(),
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                var genres = await _genreService.GetAllGenresAsync();

                viewModel.Authors = authors.ToList();
                viewModel.Genres = genres.ToList();
                viewModel.AvailabilityStatus = true;
                return View(viewModel);
            }

            await _bookService.AddBookAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            
            if (book == null)
            {
                throw new ArgumentException("Book not found!");
            }

            var authors = await _authorService.GetAllAuthorsAsync();
            var genres = await _genreService.GetAllGenresAsync();

            book.Authors = authors.ToList();
            book.Genres = genres.ToList();
            book.AvailabilityStatus = true;

            return View(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel bookViewModel)
        {
            if (id != bookViewModel.BookId)
            {
                throw new ArgumentException("Invalid ID");
            }

            if (ModelState.IsValid)
            {
                var result = await _bookService.UpdateBookAsync(bookViewModel);
                if (!result)
                {
                    throw new ArgumentException("Book not found!");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Lend(int bookId)
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

            var success = await _lendingService.LendBookAsync(bookId, member.MemberId);

            if (!success)
            {
                ModelState.AddModelError("", "Cannot lend this book right now.");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
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
            var viewModel = new BookViewModel
            {
                Authors = new List<Author>(),
                Genres = new List<Genre>()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Authors = new List<Author>();
                viewModel.Genres = new List<Genre>();
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
    }
}

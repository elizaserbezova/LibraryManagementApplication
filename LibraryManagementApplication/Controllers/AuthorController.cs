using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _authorService.AddAuthorAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                throw new ArgumentException("Author not found!");
            }
            return View(author);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AuthorViewModel viewModel)
        {
            if (id != viewModel.AuthorId)
            {
                throw new ArgumentException("Operation not allowed!");
            }

            if (ModelState.IsValid)
            {
                await _authorService.UpdateAuthorAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

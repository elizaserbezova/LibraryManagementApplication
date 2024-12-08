using AutoMapper;
using LibraryManagementApplication.Data.Models;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authorsDb = await _authorService.GetAllAuthorsAsync();
            var authors = _mapper.Map<IEnumerable<AuthorViewModel>>(authorsDb);

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
                var author = _mapper.Map<Author>(viewModel);
                await _authorService.AddAuthorAsync(author);
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

            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return View(viewModel);
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
                var author = _mapper.Map<Author>(viewModel);
                await _authorService.UpdateAuthorAsync(author);
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
            var viewModel = _mapper.Map<AuthorViewModel>(author);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int AuthorId)
        {
            await _authorService.DeleteAuthorAsync(AuthorId);
            return RedirectToAction(nameof(Index));
        }
    }
}

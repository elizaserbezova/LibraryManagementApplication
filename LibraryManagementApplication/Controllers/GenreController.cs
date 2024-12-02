using LibraryManagementApplication.Services.Data;
using LibraryManagementApplication.Services.Data.Interfaces;
using LibraryManagementApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementApplication.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService service;

        public GenreController(IGenreService _service)
        {
            service = _service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await service.GetAllGenresAsync();
            return View(genres);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var genre = await service.GetGenreByIdAsync(id);

            if (genre == null)
            {
                throw new ArgumentException("Genre not found!");
            }

            return View(genre);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                await service.AddGenreAsync(genreViewModel);
                return RedirectToAction(nameof(Index));
            }

            return View(genreViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var genre = await service.GetGenreByIdAsync(id);

            if (genre == null)
            {
                throw new ArgumentException("Genre not found!");
            }
            return View(genre);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GenreViewModel genreViewModel)
        {
            if (id != genreViewModel.GenreId)
            {
                throw new ArgumentException("Not allowed!");
            }

            if (ModelState.IsValid)
            {
                var result = await service.UpdateGenreAsync(genreViewModel);

                if (!result)
                {
                    throw new ArgumentException("Genre not found!");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(genreViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await service.GetGenreByIdAsync(id);

            if (genre == null)
            {
                throw new ArgumentException("Genre not found!");
            }

            return View(genre);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var result = await service.DeleteGenreAsync(id);

            if (!result)
            {
                throw new ArgumentException("Genre not found!");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

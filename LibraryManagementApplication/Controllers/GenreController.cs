using AutoMapper;
using LibraryManagementApplication.Data.Models;
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
        private readonly IMapper _mapper;

        public GenreController(IGenreService _service, IMapper mapper)
        {
            service = _service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genresDb = await service.GetAllGenresAsync();
            var genresViewModel = _mapper.Map<IEnumerable<GenreViewModel>>(genresDb);
            return View(genresViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var genre = await service.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return View(viewModel);
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
                var genre = _mapper.Map<Genre>(genreViewModel);
                await service.AddGenreAsync(genre);
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
                return NotFound();
            }
            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GenreViewModel genreViewModel)
        {
            if (id != genreViewModel.GenreId)
            {
                return StatusCode(500);
            }

            if (ModelState.IsValid)
            {
                var genre = _mapper.Map<Genre>(genreViewModel);
                var result = await service.UpdateGenreAsync(genre);

                if (!result)
                {
                    return NotFound();
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
                return NotFound();
            }
            var viewModel = _mapper.Map<GenreViewModel>(genre);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int GenreId)
        {
            var result = await service.DeleteGenreAsync(GenreId);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

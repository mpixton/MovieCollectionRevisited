using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieCollectionRevisited.DAL;
using MovieCollectionRevisited.Models;
using MovieCollectionRevisited.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionRevisited.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("GET on Index.");
            return View();
        }

        public IActionResult Podcasts()
        {
            _logger.LogDebug("GET on Podcasts.");
            return View();
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            _logger.LogDebug("GET on AddMovie.");
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieForm form)
        {
            _logger.LogDebug($"POST on AddMovie with params: {form}.");
            if(ModelState.IsValid)
            {
                _unitOfWork.MovieRepo.Insert(form);
                _unitOfWork.Save();
                return View("FilmList");
            }

            return View(form);
        }

        public IActionResult FilmList()
        {
            _logger.LogDebug("GET on FilmList.");
            return View(from m in _unitOfWork.MovieRepo.GetAll()
                        where m.Title != "Independence Day"
                        orderby m.Title
                        select m
                        );
        }

        [HttpGet]
        public IActionResult EditMovie(object id)
        {
            _logger.LogDebug($"GET on EditMovie with params: {id}.");
            return View(_unitOfWork.MovieRepo.GetByID(id));
        }

        [HttpPost]
        public IActionResult EditMovie(MovieForm form)
        {
            _logger.LogDebug($"POST on EditMovie with params: {form}.");
            if(ModelState.IsValid)
            {
                _unitOfWork.MovieRepo.Update(form);
                _unitOfWork.Save();
                return View("FilmList");
            }
            return View(form);
        }

        [HttpGet]
        public IActionResult DeleteMovie(object id)
        {
            _logger.LogDebug($"GET on DeleteMovie with params: {id}.");
            return View(_unitOfWork.MovieRepo.GetByID(id));
        }

        [HttpGet]
        public IActionResult DeleteMovie(MovieForm form)
        {
            _logger.LogDebug($"POST on DeletMovie with params: {form}.");
            if(ModelState.IsValid)
            {
                _unitOfWork.MovieRepo.Delete(form);
                _unitOfWork.Save();
            }
            return View(form);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
        /// <summary>
        /// Used for logging actions in the Controller.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Used for all Database transactions. Ensures that each person's requests use 
        /// the same DbContext object to prevent concurrency issues.
        /// </summary>
        private IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// GET: /
        /// </summary>
        /// <returns>Index view.</returns>
        public IActionResult Index()
        {
            _logger.LogDebug("{1} on {2}", Request.Method, Request.Path.Value);
            return View();
        }

        /// <summary>
        /// GET: Home/Podcast
        /// </summary>
        /// <returns>Podcast view.</returns>
        public IActionResult Podcast()
        {
            _logger.LogDebug("{1} on {2}", Request.Method, Request.Path.Value);
            return View();
        }

        /// <summary>
        /// GET: Home/AddMovie
        /// <para>Puts the C in CRUD for the wesbite. Allows user to create movies.</para>
        /// </summary>
        /// <returns>AddMovie view with a form for adding Movies.</returns>
        [HttpGet]
        public IActionResult AddMovie()
        {
            _logger.LogDebug("{1} on {2}", Request.Method, Request.Path.Value);
            return View();
        }

        /// <summary>
        /// POST: Home/AddMovie
        /// <para>Invoked when submitting the AddMovie form. Checks for errors, converts the 
        /// MovieForm to a Movie, and saves the new object to the Db if all is well.</para>
        /// </summary>
        /// <param name="form">Contains the data for the new Movie to add.</param>
        /// <returns>If Movie added OK, <see cref="Success"/> view. Else, the same view with errors rendered.</returns>
        [HttpPost]
        public IActionResult AddMovie(MovieForm form)
        {
            _logger.LogDebug("{1} on {2} with params: {3}", Request.Method, Request.Path.Value, form);
            if(ModelState.IsValid)
            {
                // Requires an implicit cast because the MovieRepo only stores
                // Movies, not MovieForms. MovieForm contains the implicit cast 
                // operator that prevents this from throwing an error. After 
                // the implicit cast, add and save the new Movie.
                Movie movie = form;
                _unitOfWork.MovieRepo.Insert(movie);
                _unitOfWork.Save();
                _logger.LogDebug("Rerouting to Success");
                return View("Success", form.Title);
            }

            return View(form);
        }

        /// <summary>
        /// GET: Home/FilmList
        /// </summary>
        /// <returns>View with a list of all Movies in the Db.</returns>
        public IActionResult FilmList()
        {
            _logger.LogDebug("{1} on {2}", Request.Method, Request.Path.Value);
            // Will never show the movie Independence Day cause its not as American as Rocky.
            return View(from m in _unitOfWork.MovieRepo.GetAll()
                        where m.Title != "Independence Day"
                        orderby m.Title
                        select m
                        );
        }

        /// <summary>
        /// GET: Home/EditMovie/1
        /// <para>Puts the U in CRUD. Allows users to update a Movie.</para>
        /// </summary>
        /// <param name="MovieID">PK of the Movie to edit.</param>
        /// <returns>EditMovie view with a form to make changes.</returns>
        [HttpGet]
        public IActionResult EditMovie(long MovieID)
        {
            _logger.LogDebug("{1} on {2} with params: {3}", Request.Method, Request.Path.Value, MovieID);
            return View(_unitOfWork.MovieRepo.GetByID(MovieID));
        }

        /// <summary>
        /// POST: Home/EditMovie
        /// <para>Invoked when submitting changes from the EditMovie view to be saved to the Db.</para>
        /// </summary>
        /// <param name="form">Movie form with the changes to make.</param>
        /// <returns>If successful, <see cref="FilmList"/> view. Else, the EditMovie view with form 
        /// errors rendered.</returns>
        [HttpPost]
        public IActionResult EditMovieSubmit(Movie form)
        {
            _logger.LogDebug("{1} on {2} with params: {3}", Request.Method, Request.Path.Value, form);
            // Checks if the model is valid and attempts an update.
            if(ModelState.IsValid)
            {
                _unitOfWork.MovieRepo.Update(form);
                _unitOfWork.Save();
                return RedirectToAction("FilmList");
            }
            return View(form);
        }

        /// <summary>
        /// GET: Home/DeleteMovie/1
        /// <para>Puts the D in CRUD. Allows a user to delete a Movie.</para>
        /// </summary>
        /// <param name="MovieID">PK of the Movie object to delete.</param>
        /// <returns>DeleteMovie view to review the Movie data before deleting it.</returns>
        [HttpGet]
        public IActionResult DeleteMovie(long MovieID)
        {
            _logger.LogDebug("{1} on {2} with params: {3}", Request.Method, Request.Path.Value, MovieID);
            return View(_unitOfWork.MovieRepo.GetByID(MovieID));
        }

        /// <summary>
        /// POST: Home/DeleteMovie
        /// <para>Invoked when DeleteMovie is submitted. Deletes 
        /// the Movie if no exceptions are thrown.</para>
        /// </summary>
        /// <param name="form">Movie object to delete.</param>
        /// <returns><see cref="FilmList"/> if successful. Else, the same form with render errors.</returns>
        [HttpPost]
        public IActionResult DeleteMovie(Movie form)
        {
            _logger.LogDebug("{1} on {2} with params: {3}", Request.Method, Request.Path.Value, form);
            // Checks if valid object and attempts a delete.
            if(ModelState.IsValid)
            {
                _unitOfWork.MovieRepo.Delete(form.MovieID);
                _unitOfWork.Save();
                return RedirectToAction("FilmList");
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? 
                HttpContext.TraceIdentifier });
        }
    }
}

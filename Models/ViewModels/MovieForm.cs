using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionRevisited.Models.ViewModels
{
    public class MovieForm
    {
        /// <summary>
        /// Title of the Movie.
        /// </summary>
        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }

        /// <summary>
        /// Year the Movie was published.
        /// </summary>
        [Required(ErrorMessage = "Year is required!")]
        public int? Year { get; set; }

        /// <summary>
        /// Director of the Movie.
        /// </summary>
        [Required(ErrorMessage = "Director is required!")]
        public string Director { get; set; }

        /// <summary>
        /// Rating of the Movie.
        /// </summary>
        [Required(ErrorMessage = "Rating is required!")]
        public RatingTypes? Rating { get; set; }

        /// <summary>
        /// If the Movie has been edited.
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Name of the person the Movie is currrently lent to.
        /// </summary>
        [Display(Name = "Lent To")]
        public string LentTo { get; set; }

        /// <summary>
        /// Notes about the Movie.
        /// </summary>
        [StringLength(25, ErrorMessage = "Notes must be less than 25 characters!")]
        public string Notes { get; set; }

        public static implicit operator MovieForm(Movie movie) 
        {
            return new MovieForm
            {
                Director = movie.Director,
                Edited = movie.Edited,
                LentTo = movie.LentTo,
                Notes = movie.Notes,
                Rating = movie.Rating,
                Title = movie.Title,
                Year = movie.Year
            };
        }

        public static implicit operator Movie(MovieForm form)
        {
            return new Movie
            {
                Director = form.Director,
                Edited = form.Edited,
                LentTo = form.LentTo,
                Notes = form.Notes,
                Rating = form.Rating,
                Title = form.Title,
                Year = form.Year
            };
        }
    }
}

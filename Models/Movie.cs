using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionRevisited.Models
{
    public enum RatingTypes
    {
        G = 0,
        PG = 1,
        [Display(Name = "PG-13")]
        PG13 = 2,
        R = 3
    }

    public class Movie
    {
        /// <summary>
        /// Primary Key of the Movie.
        /// </summary>
        [Key]
        public long MovieID { get; set; }

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
    }
}

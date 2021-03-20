using Microsoft.EntityFrameworkCore;
using MovieCollectionRevisited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionRevisited.DAL
{
    public class RevisitedDbContext : DbContext
    {
        public RevisitedDbContext(DbContextOptions<RevisitedDbContext> options) : base (options)
        { }

        /// <summary>
        /// Loads all Movies from the Database into memory for server processing.
        /// </summary>
        public DbSet<Movie> Movies { get; set; }
    }
}

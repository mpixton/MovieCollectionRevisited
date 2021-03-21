using MovieCollectionRevisited.Models;

namespace MovieCollectionRevisited.DAL
{
    /// <summary>
    /// Unit Of Work class. Implements the IUnitOfWork members.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// DbContext to be shared by all Repos in this Unit Of Work. Prevents concurrency issues.
        /// </summary>
        private RevisitedDbContext _context;

        /// <summary>
        /// Repo for interacting with all Movie objects.
        /// </summary>
        private GenericRepo<Movie> _movieRepo;

        public UnitOfWork(RevisitedDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// If MovieRepo Doesn't exists, create a new one with the DbContext provided by dependency 
        /// injection. If already exists, return the current one. Ensures that all Repos are sharing
        /// the same DbContext so that changes to mulitple Repos are saved at the same time.
        /// </summary>
        public GenericRepo<Movie> MovieRepo
        {
            get
            {
                if(_movieRepo is null)
                {

                    _movieRepo = new GenericRepo<Movie>(_context);
                }
                return _movieRepo;
            }
        }

        /// <summary>
        /// Saves the chagnes across all Repos in one go.
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

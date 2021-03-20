using MovieCollectionRevisited.Models;

namespace MovieCollectionRevisited.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private RevisitedDbContext _context;
        private GenericRepo<Movie> _movieRepo;

        public UnitOfWork(RevisitedDbContext context)
        {
            _context = context;
        }

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


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

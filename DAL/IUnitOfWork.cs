using MovieCollectionRevisited.Models;

namespace MovieCollectionRevisited.DAL
{
    public interface IUnitOfWork
    {
        GenericRepo<Movie> MovieRepo { get; }

        void Save();
    }
}
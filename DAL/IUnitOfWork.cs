using MovieCollectionRevisited.Models;

namespace MovieCollectionRevisited.DAL
{
    /// <summary>
    /// Defines the members for the Unit Of Work class. 
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Generic Repo of Movie objects.
        /// </summary>
        GenericRepo<Movie> MovieRepo { get; }

        /// <summary>
        /// Saves the changes made to the Repos in this Unit Of Work to the Db.
        /// </summary>
        void Save();
    }
}
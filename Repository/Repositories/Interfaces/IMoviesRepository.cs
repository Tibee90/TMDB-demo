using Repository.Entities;
using System.Collections.Generic;

namespace Repository.Repositories.Interfaces
{
    public interface IMoviesRepository
    {
        IEnumerable<int> GetMovieIds();
        void Save(IEnumerable<Movie> moviesToAdd);
        void Save(Movie moviesToAdd);
        IEnumerable<Movie> GetAll();
        Movie GetById(int id);
    }
}

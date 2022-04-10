using Repository.Entities;
using Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class MoviesRepository : IMoviesRepository
    {
        private readonly DataContext _context;

        public MoviesRepository(DataContext dataContext) {
            _context = dataContext;
        }

        public IEnumerable<int> GetMovieIds()
        {
            return _context.Movies.Select(x => x.Id);
        }

        public void Delete(int id) {
            var movie = new Movie { Id = id };
            _context.Movies.Attach(movie);
            _context.Remove(movie);
            _context.SaveChanges();
        }

        public void Delete(IEnumerable<int> ids) {
            _context.RemoveRange(ids.Select(id => new Movie { Id = id }));
            _context.SaveChanges();
        }

        public void Save(IEnumerable<Movie> moviesToAdd) {           
            _context.AddRange(moviesToAdd);
            _context.SaveChanges();
        }

        public void Save(Movie movie)
        {
            _context.Add(movie);
            _context.SaveChanges();
        }

        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies;
        }

        public Movie GetById(int id)
        {
            return _context.Movies.Single(x => x.Id == id);
        }
    }
}

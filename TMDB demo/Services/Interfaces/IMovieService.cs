using Repository.Entities;
using System.Collections.Generic;
using TMDB_demo.Models;

namespace TMDB_demo.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<int> TopRatedMovieIds();
        ChangedMovieIds GetChangedMovieIds();
        void SaveMovies(IList<Movie> movies);
        void SaveMovie(Movie movie);
        Genre GetGenreById(int id);
        Director GetDirectorById(int id);
        Movie GetMovie(int id);
    }
}

using System.Collections.Generic;
using TMDB_demo.Models.TMDB_models;

namespace TMDB_demo.Services.Interfaces
{
    public interface ITmdbDataService
    {
        IEnumerable<int> GetTopRatedMovieIds();
        MovieWithCredits GetMovieDetails(int id);
        TmdbDirector GetDirectorDetails(int id);
        IEnumerable<TmdbGenre> GetGenres();
    }
}

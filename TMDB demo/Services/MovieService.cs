using Repository.Entities;
using Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMDB_demo.Models;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMoviesRepository _movieRepository;
        private readonly IGenresRepository _genreRepository;
        private readonly IDirectorsRepository _directorRepository;
        private readonly ITmdbDataService _tmdbDataService;

        public MovieService(
            IMoviesRepository movieRepository,  
            IGenresRepository genreRepository,
            IDirectorsRepository directorRepository,
            ITmdbDataService tmdbService) 
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _directorRepository = directorRepository;
            _tmdbDataService = tmdbService;
        }

        public IEnumerable<int> TopRatedMovieIds() {
            return _tmdbDataService.GetTopRatedMovieIds();
        }

        public ChangedMovieIds GetChangedMovieIds()
        {
            var idsFromDb = _movieRepository.GetMovieIds();
            var idsFromApi = _tmdbDataService.GetTopRatedMovieIds();

            return new ChangedMovieIds {                
                NewIdsToAdd = idsFromApi.Except(idsFromDb),
                NotInTopRatedIds = idsFromDb.Except(idsFromApi)
            };           
        }

        public Genre GetGenreById(int id)
        {
            return _genreRepository.GetById(id);
        }

        public Director GetDirectorById(int id)
        {
            return _directorRepository.GetById(id);
        }

        public void SaveMovies(IList<Movie> movies) {
            _movieRepository.Save(movies);
        }

        public void SaveMovie(Movie movie) {
            _movieRepository.Save(movie);
        }

        public Movie GetMovie(int id) {
            return _movieRepository.GetById(id);
        }
    }
}

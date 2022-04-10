using AutoMapper;
using Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using TMDB_demo.Models.TMDB_models;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo.Services
{
    public class UpdateService : IUpdateSevice
    {
        private readonly object changeLock = new();

        private readonly ITmdbDataService _tmdbService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public UpdateService(
            ITmdbDataService tmdbService,
            IMovieService movieService,
            IMapper mapper) 
        {
            _tmdbService = tmdbService;
            _movieService = movieService;
            _mapper = mapper;
        }

        public void HandleChanges() {

            lock (changeLock) {
                //Add new movies to db
                var changedMovieIds = _movieService.GetChangedMovieIds();
                HandleNewMovies(changedMovieIds.NewIdsToAdd);

                HandleNotTopRatedAnyMoreMovies(changedMovieIds.NotInTopRatedIds);
            }
            
        }

        private void HandleNewMovies(IEnumerable<int> ids) {
            var moviesToAdd = GetNewMovies(ids);            
            foreach (var movieModel in moviesToAdd) {
                var movie = _mapper.Map<Movie>(movieModel);
                movie.IsTopRated = true;
                foreach (var genreModel in movieModel.Genres) {                    
                    movie.Genres.Add(_movieService.GetGenreById(genreModel.Id) ?? _mapper.Map<Genre>(genreModel));                   
                }

                foreach (var directorModel in movieModel.Directors)
                {                     
                    movie.Directors.Add(_movieService.GetDirectorById(directorModel.Id) ?? _mapper.Map<Director>(directorModel));                    
                }

                _movieService.SaveMovie(movie);
            }
        }

        private void HandleNotTopRatedAnyMoreMovies(IEnumerable<int> ids) {
            foreach (var id in ids) {
                var movie = _movieService.GetMovie(id);
                movie.IsTopRated = false;
                _movieService.SaveMovie(movie);
            }
        }

        private IEnumerable<TmdbMovie> GetNewMovies(IEnumerable<int> newIds) {
            var movies = new List<MovieWithCredits>();
            foreach (var id in newIds)
            {
                var movie = _tmdbService.GetMovieDetails(id);
                var directorIds = movie.Credits.Crew.Where(x => x.Job == "Director").Select(x => x.Id);

                var directors = new List<TmdbDirector>();
                foreach (var directorId in directorIds)
                {
                    directors.Add(_tmdbService.GetDirectorDetails(directorId));
                }

                movie.Directors = directors;
                movies.Add(movie);
            }
            return movies;
        }
    }
}

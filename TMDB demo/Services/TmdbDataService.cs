using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDB_demo.Extensions;
using TMDB_demo.Models.TMDB_models;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo.Services
{
    public class TmdbDataService : ITmdbDataService
    {
        private const int TopXMovie = 210;

        private readonly ITmdbApiService _tmdbApiService;
        private readonly IConfiguration Configuration;

        public TmdbDataService(
            ITmdbApiService dataService,
            IConfiguration configuration) 
        {
            _tmdbApiService = dataService;
            Configuration = configuration;
        }

        public TmdbDirector GetDirectorDetails(int id)
        {
            var personParemeters = new Dictionary<string, string> {
                { "person_id", id.ToString() }
            };
            return _tmdbApiService.GetData<TmdbDirector>(Configuration.GetPersonUrl() , personParemeters).GetAwaiter().GetResult();
        }

        public IEnumerable<int> GetTopRatedMovieIds()
        {
            var movieIds = new List<int>();
            var page = 1;
            while (movieIds.Count < TopXMovie)
            {
                var parameters = new Dictionary<string, string>
                {
                    { "page", page.ToString()}
                };
                var result = _tmdbApiService.GetData<TopRated>(Configuration.GetTopRatedUrl(), parameters).GetAwaiter().GetResult();
                movieIds.AddRange(result.Results.Select(x => x.Id));
                page++;
            }
            return movieIds.Take(TopXMovie);
        }

        public MovieWithCredits GetMovieDetails(int id)
        {
            var movieParameters = new Dictionary<string, string> {
                { "movie_id", id.ToString() },
                { "append_to_response", "credits,genres"}
            };
            return _tmdbApiService.GetData<MovieWithCredits>(Configuration.GetMovieUrl(), movieParameters).GetAwaiter().GetResult();
        }

        public IEnumerable<TmdbGenre> GetGenres() {
            
            return _tmdbApiService.GetData<TmdbGenres>(Configuration.GetGenreListUrl()).GetAwaiter().GetResult().Genres;            
        }
    }
}

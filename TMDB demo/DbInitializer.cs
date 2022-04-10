using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMDB_demo.Services.Interfaces;

namespace TMDB_demo
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider services) {
            var context = services.GetRequiredService<DataContext>();            

            context.Database.EnsureCreated();
            if (!context.Genres.Any())
            {
                var tmdbService = services.GetRequiredService<ITmdbDataService>();
                var mapper = services.GetRequiredService<IMapper>();
                var genreRepository = services.GetRequiredService<IGenresRepository>();

                var genreModels = tmdbService.GetGenres();
                var genres = mapper.Map<IEnumerable<Genre>>(genreModels);
                genreRepository.SaveAll(genres);
            }
        }        
    }
}

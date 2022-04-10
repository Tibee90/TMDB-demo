using Microsoft.Extensions.Configuration;

namespace TMDB_demo.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetApiKey(this IConfiguration configuration) {
            return configuration["Tmdb:ApiKey"];
        }

        public static string GetPersonUrl(this IConfiguration configuration)
        {
            return configuration["Tmdb:PersonUrl"];
        }

        public static string GetTopRatedUrl(this IConfiguration configuration)
        {
            return configuration["Tmdb:TopRatedUrl"];
        }

        public static string GetMovieUrl(this IConfiguration configuration)
        {
            return configuration["Tmdb:MovieUrl"];
        }

        public static string GetGenreListUrl(this IConfiguration configuration)
        {
            return configuration["Tmdb:GenreListUrl"];
        }

        public static string GetApiVersion(this IConfiguration configuration)
        {
            return configuration["Tmdb:ApiVersion"];
        }

        public static string GetBaseUrl(this IConfiguration configuration)
        {
            return configuration["Tmdb:BaseUrl"];
        }

        public static string GetUpdateDatabaseJobCronExpression(this IConfiguration configuration) {
            return configuration["JobCronExpressions:UpdateDatabaseJob"];
        }
    }
}

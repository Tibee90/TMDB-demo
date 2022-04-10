using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TMDB_demo.Models.TMDB_models
{
    public class TmdbMovie
    {
        public TmdbMovie() {
            Directors = new List<TmdbDirector>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        [JsonPropertyName("runtime")]
        public int Length { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterUrl { get; set; }
        [JsonPropertyName("vote_average")]
        public decimal VoteAverage { get; set; }
        [JsonPropertyName("vote_count")]
        public decimal VoteCount { get; set; }
        [JsonPropertyName("genres")]
        public IEnumerable<TmdbGenre> Genres { get; set; }
        public string Url { get; set; }        
        [JsonIgnore]
        public ICollection<TmdbDirector> Directors { get; set; }
    }
}

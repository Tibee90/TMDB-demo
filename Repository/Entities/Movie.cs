using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Entities
{
    public class Movie
    {
        public Movie() {
            Genres = new HashSet<Genre>();
            Directors = new HashSet<Director>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        //public int TmdbId { get; set; }
        public decimal TmdbVoteAverage { get; set; }
        public int TmdbVoteCount { get; set; }
        public string TmdbUrl { get; set; }
        public string PosterUrl { get; set; }
        public int Length { get; set; }

        public bool IsTopRated { get; set; }
        public virtual ICollection<Director> Directors { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }


    }
}

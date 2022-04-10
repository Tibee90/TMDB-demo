using System;

namespace TMDB_demo.Models.TMDB_models
{
    public class TmdbDirector : Person
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime? BirthDay { get; set; }
    }
}

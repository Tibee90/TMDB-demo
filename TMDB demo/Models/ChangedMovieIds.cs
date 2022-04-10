using System.Collections.Generic;

namespace TMDB_demo.Models
{
    public class ChangedMovieIds
    {
        public IEnumerable<int> NewIdsToAdd { get; set; }
        public IEnumerable<int> NotInTopRatedIds { get; set; }
    }
}

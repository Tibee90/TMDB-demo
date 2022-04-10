using Repository.Entities;
using Repository.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Repositories
{
    public class GenresRespository : IGenresRepository
    {
        private readonly DataContext _context;

        public GenresRespository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public void SaveAll(IEnumerable<Genre> genres)
        {
            _context.Genres.AddRange(genres);
            _context.SaveChanges();
        }

        public Genre GetById(int id) {
            return _context.Genres.SingleOrDefault(x => x.Id == id);
        }
    }
}

using Repository.Entities;
using System.Collections.Generic;

namespace Repository.Repositories.Interfaces
{
    public interface IGenresRepository
    {
        void SaveAll(IEnumerable<Genre> genres);
        Genre GetById(int id);
    }
}

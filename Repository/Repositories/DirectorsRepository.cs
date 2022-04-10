using Repository.Entities;
using Repository.Repositories.Interfaces;
using System;
using System.Linq;

namespace Repository.Repositories
{
    public class DirectorsRepository : IDirectorsRepository
    {
        private readonly DataContext _context;

        public DirectorsRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public Director GetById(int id)
        {
            return _context.Directors.SingleOrDefault(x => x.Id == id);
        }
    }
}

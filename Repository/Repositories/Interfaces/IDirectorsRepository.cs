using Repository.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IDirectorsRepository
    {
        Director GetById(int id);
    }
}

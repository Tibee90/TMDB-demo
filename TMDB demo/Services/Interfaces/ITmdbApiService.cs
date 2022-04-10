using System.Collections.Generic;
using System.Threading.Tasks;

namespace TMDB_demo.Services.Interfaces
{
    public interface ITmdbApiService
    {
        Task<T> GetData<T>(string path, Dictionary<string, string> parameters = null);
    }
}

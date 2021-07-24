using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.IRepositories
{
    public interface ISizeRepository
    {
        Task<IEnumerable<Size>> GetSizes();
        Task<Size> GetSize(int sizeId);
    }
}

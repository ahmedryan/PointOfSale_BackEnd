using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.IRepositories
{
    public interface IFitRepository
    {
        Task<IEnumerable<Fit>> GetFits();
        Task<Fit> GetFit(int fitId);
    }
}

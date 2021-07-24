using System;
using System.Threading.Tasks;

namespace API.Repositories.IRepositories
{
    public interface IReportRepository
    {
        Task<Object> GetReport();
    }
}

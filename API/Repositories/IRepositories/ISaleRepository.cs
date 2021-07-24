using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;

namespace API.Repositories.IRepositories
{
    public interface ISaleRepository
    {
        Task<List<SaleWithProductDto>> GetSales();
        Task<Customer> GetSale(int saleId);
        Task<bool> CreateSale(Sale createSale);
    }
}

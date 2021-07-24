using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Http;

namespace API.Repositories.IRepositories
{
    public interface IProductRepository
    {
        Task<ProductCount> GetProducts(IQueryCollection queryCollection);
        Task<Product> GetProduct(int productId);
        Task<bool> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<bool> ProductExists(int productId);
        Task<bool> ProductExists(string productId);
        Task<bool> Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _db;

        public SaleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<List<SaleWithProductDto>> GetSales()
        {
            var sales = await _db.Sales.Include(x => x.Customer).ToListAsync();
            var products = await _db.Products.Include(x => x.Category).ToListAsync();

            List<SaleWithProductDto> result = new List<SaleWithProductDto>();

            foreach (var product in products)
            {
                foreach (var sale in sales)
                {
                    if (product.SaleId == sale.Id)
                    {
                        var temp = new SaleWithProductDto()
                        {
                            ProductId = product.Pid,
                            ProductCategory = product.Category.Name,
                            CustomerName = sale.Customer.Name,
                            CustomerContact = sale.Customer.ContactNumber,
                            SaleDate = sale.SaleDate,
                            Price = product.Price
                        };
                        
                        result.Add(temp);
                    }
                }
            }

            result = result.OrderBy(x => x.SaleDate).ToList();
            
            return result;
        }

        public Task<Customer> GetSale(int saleId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CreateSale(Sale saleToCreate)
        {
            saleToCreate.SaleDate = DateTime.Now;
            
            await _db.Sales
                .AddAsync(saleToCreate);

            return await Save();
        }

        // public async Task<int> CustomerExists(string contactNumber)
        // {
        //     var customer = await _db.Customers
        //         .Where(x => x.ContactNumber == contactNumber)
        //         .FirstOrDefaultAsync();
        //     
        //     if (customer == null) return 0;
        //     
        //     return customer.Id;
        // }

        public async Task<bool> Save()
        {
            return await _db
                .SaveChangesAsync() >= 0;
        }
    }
}

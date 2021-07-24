using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _db.Customers.ToListAsync();
        }

        public Task<Customer> GetCustomer(int customerId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> CreateCustomer(Customer customerToCreate)
        {
            await _db.Customers
                .AddAsync(customerToCreate);

            return await Save();
        }

        public async Task<int> CustomerExists(string contactNumber)
        {
            var customer = await _db.Customers
                .Where(x => x.ContactNumber == contactNumber)
                .FirstOrDefaultAsync();
            
            if (customer == null) return 0;
            
            return customer.Id;
        }

        public async Task<bool> Save()
        {
            return await _db
                .SaveChangesAsync() >= 0;
        }
    }
}

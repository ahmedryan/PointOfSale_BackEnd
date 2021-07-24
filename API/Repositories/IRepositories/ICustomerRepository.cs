using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Http;

namespace API.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int customerId);
        Task<bool> CreateCustomer(Customer createCustomer);
        Task<int> CustomerExists(string contactNumber);
    }
}

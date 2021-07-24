using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;

namespace API.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int productCategoryId);
    }
}

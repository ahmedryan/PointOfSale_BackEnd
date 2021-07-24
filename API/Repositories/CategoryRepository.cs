using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return await _db.Categories
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}

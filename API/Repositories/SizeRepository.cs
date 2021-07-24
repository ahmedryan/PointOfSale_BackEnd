using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SizeRepository : ISizeRepository
    {
        private readonly ApplicationDbContext _db;

        public SizeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await _db.Sizes.ToListAsync();
        }

        public async Task<Size> GetSize(int sizeId)
        {
            return await _db.Sizes
                .FirstOrDefaultAsync(x => x.Id == sizeId);
        }
    }
}

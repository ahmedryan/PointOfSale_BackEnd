using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _db;

        public ColorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _db.Colors.ToListAsync();
        }

        public async Task<Color> GetColor(int colorId)
        {
            return await _db.Colors
                .FirstOrDefaultAsync(x => x.Id == colorId);
        }
    }
}

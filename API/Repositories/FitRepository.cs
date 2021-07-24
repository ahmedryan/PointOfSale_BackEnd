using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class FitRepository : IFitRepository
    {
        private readonly ApplicationDbContext _db;

        public FitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Fit>> GetFits()
        {
            return await _db.Fits.ToListAsync();
        }

        public async Task<Fit> GetFit(int productFitId)
        {
            return await _db.Fits
                .FirstOrDefaultAsync(x => x.Id == productFitId);
        }
    }
}

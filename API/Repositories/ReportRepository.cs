using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _db;

        public ReportRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<Object> GetReport()
        {
            var products = await _db.Products
                .Include(x => x.Category)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Fit)
                .ToListAsync();

            var categories = await _db.Categories.ToListAsync();
            var colors = await _db.Colors.ToListAsync();
            var sizes = await _db.Sizes.ToListAsync();
            var fits = await _db.Fits.ToListAsync();
            
            Dictionary<string, int> categoryLeftCount = new Dictionary<string, int>(); 
            Dictionary<string, int> colorLeftCount = new Dictionary<string, int>(); 
            Dictionary<string, int> sizeLeftCount = new Dictionary<string, int>(); 
            Dictionary<string, int> fitLeftCount = new Dictionary<string, int>(); 
            Dictionary<string, int> categorySoldCount = new Dictionary<string, int>(); 
            Dictionary<string, int> colorSoldCount = new Dictionary<string, int>(); 
            Dictionary<string, int> sizeSoldCount = new Dictionary<string, int>(); 
            Dictionary<string, int> fitSoldCount = new Dictionary<string, int>(); 
            
            categories.ForEach(category =>
            {
                if (!categoryLeftCount.ContainsKey(category.Name))
                {
                    categoryLeftCount.Add(category.Name, 0);
                    categorySoldCount.Add(category.Name, 0);
                }
            });
            
            colors.ForEach(color =>
            {
                if (!colorLeftCount.ContainsKey(color.Name))
                {
                    colorLeftCount.Add(color.Name, 0);
                    colorSoldCount.Add(color.Name, 0);
                }
            });
            
            sizes.ForEach(size =>
            {
                if (!sizeLeftCount.ContainsKey(size.Name))
                {
                    sizeLeftCount.Add(size.Name, 0);
                    sizeSoldCount.Add(size.Name, 0);
                }
            });
            
            fits.ForEach(fit =>
            {
                if (!fitLeftCount.ContainsKey(fit.Name))
                {
                    fitLeftCount.Add(fit.Name, 0);
                    fitSoldCount.Add(fit.Name, 0);
                }
            });
            
            products.ForEach(product =>
            {
                if (categoryLeftCount.ContainsKey(product.Category.Name) && product.SaleId == null)
                {
                    categoryLeftCount[product.Category.Name]++;
                }
                if (categorySoldCount.ContainsKey(product.Category.Name) && product.SaleId != null)
                {
                    categorySoldCount[product.Category.Name]++;
                }
                // else
                // {
                //     categoryDetailCount.Add(product.Category.Name, 1);
                // }
                
                if (colorLeftCount.ContainsKey(product.Color.Name)  && product.SaleId == null)
                {
                    colorLeftCount[product.Color.Name]++;
                }
                if (colorSoldCount.ContainsKey(product.Color.Name)  && product.SaleId != null)
                {
                    colorSoldCount[product.Color.Name]++;
                }
                // else
                // {
                //     colorDetailCount.Add(product.Color.Name, 1);
                // }
                
                if (sizeLeftCount.ContainsKey(product.Size.Name) && product.SaleId == null)
                {
                    sizeLeftCount[product.Size.Name]++;
                }
                if (sizeSoldCount.ContainsKey(product.Size.Name) && product.SaleId != null)
                {
                    sizeSoldCount[product.Size.Name]++;
                }
                // else
                // {
                //     sizeDetailCount.Add(product.Size.Name, 1);
                // }
                
                if (fitLeftCount.ContainsKey(product.Fit.Name) && product.SaleId == null)
                {
                    fitLeftCount[product.Fit.Name]++;
                }
                if (fitSoldCount.ContainsKey(product.Fit.Name) && product.SaleId != null)
                {
                    fitSoldCount[product.Fit.Name]++;
                }
                // else
                // {
                //     fitDetailCount.Add(product.Fit.Name, 1);
                // }
            });

            // foreach (KeyValuePair<string, int> e in categoryLeftCount)
            // {
            //     Console.WriteLine(e.Key, e.Value);
            // }
            
            return new {
                categoryLeftCount = categoryLeftCount, 
                colorLeftCount = colorLeftCount, 
                sizeLeftCount = sizeLeftCount, 
                fitLeftCount = fitLeftCount,
                categorySoldCount = categorySoldCount, 
                colorSoldCount = colorSoldCount, 
                sizeSoldCount = sizeSoldCount, 
                fitSoldCount = fitSoldCount,
            } ;
        }
    }
}

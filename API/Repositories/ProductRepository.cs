using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // public async Task<IEnumerable<Product>> GetProducts()
        // {
        //     return await _db.Products
        //         .Skip(2*4) // (pageIndex-1)*pageSize
        //         .Take(4) // pageSize
        //         .OrderBy(x => x.Price)
        //         .ToListAsync();
        // }

        public async Task<ProductCount> GetProducts(IQueryCollection queryCollection)
        {
            var pid = queryCollection["pid"].ToString();
            var categoryId = int.Parse(queryCollection["categoryId"]) ;
            var fitId = int.Parse(queryCollection["fitId"]);
            var sizeId = int.Parse(queryCollection["sizeId"]);
            var colorId = int.Parse(queryCollection["colorId"]);
            // var lowprice = int.Parse(queryCollection["lowprice"]);
            // var highprice = int.Parse(queryCollection["highprice"]);
            var days = queryCollection["days"].ToString();
            var pageIndex = queryCollection["pageIndex"].ToString();
            var pageSize = queryCollection["pageSize"].ToString();
            var sortPrice = queryCollection["sortPrice"].ToString();
            var sortDate = queryCollection["sortDate"].ToString();
            var search = queryCollection["search"].ToString();

            var products = await _db.Products
                .Include(x => x.Category)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Fit)
                .ToListAsync();

            if (search != "")
            {
                products = products.Where(x => 
                    x.Pid.Contains(search) || 
                    x.Category.Name.Contains(search) || 
                    x.Color.Name.Contains(search) || 
                    x.Fit.Name.Contains(search) ||
                    x.Size.Name.Contains(search)
                ).ToList();
            }
            if (pid != "")
            {
                products = products.FindAll(x => x.Pid == pid);
            }
            if (categoryId != 0)
            {
                products = products.FindAll(x => x.CategoryId == categoryId);
            }
            if (fitId != 0)
            {
                products = products.FindAll(x => x.FitId == fitId);
            }
            if (sizeId != 0)
            {
                products = products.FindAll(x => x.SizeId == sizeId);
            }
            if (colorId != 0)
            {
                products = products.FindAll(x => x.ColorId == colorId);
            }
            // if (lowprice != -1)
            // {
            //     products = products.FindAll(x => x.Price >= lowprice);
            // }
            // if (highprice != -1)
            // {
            //     products = products.FindAll(x => x.Price <= highprice);
            // }
            if (days != "")
            {
                int d = int.Parse(days) * (-1);
                // Console.WriteLine(DateTime.Now.AddDays(d));
                products = products.FindAll(x => x.DateOfPurchase >= DateTime.Now.AddDays(d));
            }
            if (sortPrice != "" && sortDate != "")
            {
                if (sortPrice == "asc" && sortDate == "asc")
                {
                    products = products
                        .OrderBy(x => x.Price)
                        .ThenBy(x => x.DateOfPurchase)
                        .ToList();
                }
                else if (sortPrice == "asc" && sortDate == "desc")
                {
                    products = products
                        .OrderBy(x => x.Price)
                        .ThenByDescending(x => x.DateOfPurchase)
                        .ToList();
                }
                else if (sortPrice == "desc" && sortDate == "asc")
                {
                    products = products
                        .OrderByDescending(x => x.Price)
                        .ThenBy(x => x.DateOfPurchase)
                        .ToList();
                }
                else if (sortPrice == "desc" && sortDate == "desc")
                {
                    products = products
                        .OrderByDescending(x => x.Price)
                        .ThenByDescending(x => x.DateOfPurchase)
                        .ToList();
                }
            }
            else if (sortDate != "" || sortPrice != "")
            {
                switch (sortDate)
                {
                    case "asc":
                        products = products
                            .OrderBy(x => x.DateOfPurchase)
                            .ToList();
                        break;
                    case "desc":
                        products = products
                            .OrderByDescending(x => x.DateOfPurchase)
                            .ToList();
                        break;
                }
                switch (sortPrice)
                {
                    case "asc":
                        products = products
                            .OrderBy(x => x.Price)
                            .ToList();
                        break;
                    case "desc":
                        products = products
                            .OrderByDescending(x => x.Price)
                            .ToList();
                        break;
                }
            }

            // unsold products
            products = products.FindAll(x => x.SaleId == null);
            
            var count = products.Count;
            
            if ((pageIndex != "" && pageSize != "") && (pageIndex != "0" && pageSize != "0"))
            {
                int index = int.Parse(pageIndex);
                int _size = int.Parse(pageSize);
                products = products
                    .Skip((index - 1) * _size) // (pageIndex-1)*pageSize
                    .Take(_size) // pageSize
                    .ToList();
            }

            return new ProductCount(products, count);
        }
        
        public async Task<Product> GetProduct(int productId)
        {
            return await _db.Products
                .FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<bool> CreateProduct(Product product)
        {
            product.DateOfPurchase = DateTime.Now;
            
            await _db.Products
                .AddAsync(product);
            return await Save();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _db.Products
                .Update(product);
            
            return await Save();
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _db.Products
                .Remove(product);
            return await Save();
        }

        public async Task<bool> ProductExists(int productId)
        {
            return await _db.Products
                .AnyAsync(x => x.Id == productId);
        }

        public async Task<bool> ProductExists(string productId)
        {
            return await _db.Products
                .AnyAsync(x => x.Pid == productId);
        }
        
        public async Task<bool> Save()
        {
            return await _db
                .SaveChangesAsync() >= 0;
        }
    }
}

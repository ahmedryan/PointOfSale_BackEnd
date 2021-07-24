using API.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext db, ILoggerFactory loggerFactory)
        {
            try
            {   
                // Categories
                if (!db.Categories.Any())
                {
                    var categoriesData = await File.ReadAllTextAsync("./Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var item in categories)
                    {
                        db.Categories.Add(item);
                    }

                    db.Database.OpenConnection();
                    try
                    {
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories ON;");
                        await db.SaveChangesAsync();
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Categories OFF;");
                    }
                    finally
                    {
                        db.Database.CloseConnection();
                    }
                }
                // Colors
                if (!db.Colors.Any())
                {
                    var colorsData = await File.ReadAllTextAsync("./Data/SeedData/colors.json");
                    var colors = JsonSerializer.Deserialize<List<Color>>(colorsData);

                    foreach (var item in colors)
                    {
                        db.Colors.Add(item);
                    }

                    db.Database.OpenConnection();
                    try
                    {
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Colors ON;");
                        await db.SaveChangesAsync();
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Colors OFF;");
                    }
                    finally
                    {
                        db.Database.CloseConnection();
                    }
                }
                // Sizes
                if (!db.Sizes.Any())
                {
                    var sizesData = await File.ReadAllTextAsync("./Data/SeedData/sizes.json");
                    var sizes = JsonSerializer.Deserialize<List<Size>>(sizesData);

                    foreach (var item in sizes)
                    {
                        db.Sizes.Add(item);
                    }

                    db.Database.OpenConnection();
                    try
                    {
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Sizes ON;");
                        await db.SaveChangesAsync();
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Sizes OFF;");
                    }
                    finally
                    {
                        db.Database.CloseConnection();
                    }
                }
                // Fits
                if (!db.Fits.Any())
                {
                    var fitsData = await File.ReadAllTextAsync("./Data/SeedData/fits.json");
                    var fits = JsonSerializer.Deserialize<List<Fit>>(fitsData);

                    foreach (var item in fits)
                    {
                        db.Fits.Add(item);
                    }

                    db.Database.OpenConnection();
                    try
                    {
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fits ON;");
                        await db.SaveChangesAsync();
                        db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Fits OFF;");
                    }
                    finally
                    {
                        db.Database.CloseConnection();
                    }
                    // Products
                    if (!db.Products.Any())
                    {
                        var productsData = await File.ReadAllTextAsync("./Data/SeedData/products.json");
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                        foreach (var item in products)
                        {
                            db.Products.Add(item);
                        }

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}

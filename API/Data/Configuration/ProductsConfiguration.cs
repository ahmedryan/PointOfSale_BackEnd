using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Configuration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(y => y.Id).IsRequired();

            builder.HasIndex(y => y.Pid).IsUnique(true);

            builder.Property(y => y.Pid).IsRequired();
            builder.Property(y => y.Price).IsRequired();
            builder.Property(y => y.DateOfPurchase).IsRequired();
            builder.Property(y => y.CategoryId).IsRequired();
            builder.Property(y => y.ColorId).IsRequired();
            builder.Property(y => y.SizeId).IsRequired();
            builder.Property(y => y.FitId).IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(y => y.CategoryId);
            builder.HasOne(x => x.Color)
                .WithMany()
                .HasForeignKey(y => y.ColorId);
            builder.HasOne(x => x.Size)
                .WithMany()
                .HasForeignKey(y => y.SizeId);
            builder.HasOne(x => x.Fit)
                .WithMany()
                .HasForeignKey(y => y.FitId);

            builder.HasOne(x => x.Sale)
                .WithMany(y => y.Products)
                .HasForeignKey(z => z.SaleId);
        }
    }
}

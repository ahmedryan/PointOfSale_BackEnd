using System;

namespace API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public double Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int FitId { get; set; }
        public Fit Fit { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }

        public int? SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}

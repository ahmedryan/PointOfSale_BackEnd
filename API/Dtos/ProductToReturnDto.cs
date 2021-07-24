using System;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int FitId { get; set; }
        public string Fit { get; set; }
        public int SizeId { get; set; }
        public string Size { get; set; }
        public int ColorId { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}

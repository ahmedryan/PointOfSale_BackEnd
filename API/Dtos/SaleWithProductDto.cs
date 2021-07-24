using System;
using API.Entities;

namespace API.Dtos
{
    public class SaleWithProductDto
    {
        public string ProductId { get; set; }
        public string ProductCategory { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public DateTime SaleDate { get; set; }
        public double Price { get; set; }
    }
}

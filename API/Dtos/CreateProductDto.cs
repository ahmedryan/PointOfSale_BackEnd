using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CreateProductDto
    {
        // [Required]
        // public int Id { get; set; }
        [Required]
        public string Pid { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int FitId { get; set; }
        [Required]
        public int SizeId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public double Price { get; set; }
        public int? SaleId { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}

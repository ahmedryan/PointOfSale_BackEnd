using System;

namespace API.Dtos
{
    public class CreateSaleDto
    {
        public DateTime SaleDate { get; set; }
        public double Bill { get; set; }
        public int CustomerId { get; set; }
    }
}

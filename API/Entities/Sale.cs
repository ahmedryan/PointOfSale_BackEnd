using System;
using System.Collections.Generic;

namespace API.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public double Bill { get; set; }
        public DateTime SaleDate { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Product> Products { get; set; }
    }
}

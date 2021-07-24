using System.Collections.Generic;
using API.Entities;

namespace API.Dtos
{
    public class ProductCount
    {
        public ProductCount(IEnumerable<Product> prods, int cnt)
        {
            Products = prods;
            Count = cnt;
        }
    
        public IEnumerable<Product> Products { get; set; }
        public int Count { get; set; }
    }
}

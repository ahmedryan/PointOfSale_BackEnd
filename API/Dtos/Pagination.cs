using System.Collections.Generic;

namespace API.Dtos
{
    public class Pagination
    {
        public Pagination(int pageIndex, int pageSize, int count, IEnumerable<ProductToReturnDto> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<ProductToReturnDto> Data { get; set; }
    }
}

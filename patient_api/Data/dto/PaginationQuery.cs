using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace patient_api.Data.dto
{
    public class PaginationQuery
    {
        private int MaxPageSize = 20;
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = MaxPageSize;
        }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}

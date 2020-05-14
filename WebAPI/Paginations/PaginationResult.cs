using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Paginations
{
    public class PaginationResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public PaginationResult(IReadOnlyList<T> items, int total)
        {
            Items = items;
            TotalCount = total;
        }       
    }
}

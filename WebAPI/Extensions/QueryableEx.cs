using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Paginations;

namespace WebAPI.Extensions
{
    public static class QueryableEx
    {
        public static async Task<PaginationResult<T>> GetPaginationResultAsync<T>(IQueryable<T> query, PaginationParam param) where T : class
        {
            if (param.SkipCount != null)
            {
                query = query.Skip((int)param.SkipCount);
            }
            if (param.MaxResultCount != null)
            {
                query = query.Take((int)param.MaxResultCount);
            }
            return new PaginationResult<T>(await query.ToListAsync(), query.Count());
        }
    }
}

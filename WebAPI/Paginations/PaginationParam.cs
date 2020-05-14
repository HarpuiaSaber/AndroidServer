using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Paginations
{
    public class PaginationParam
    {
        public int? SkipCount { get; set; }
        public int? MaxResultCount { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Paginations;

namespace WebAPI.Models.Search
{
    public class ExamSearch
    {
        public PaginationParam Param { get; set; }
        public ExamType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

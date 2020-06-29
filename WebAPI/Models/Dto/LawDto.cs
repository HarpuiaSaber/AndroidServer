using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class LawDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public LawType Type { get; set; }
        public string Punishment { get; set; }
    }
}

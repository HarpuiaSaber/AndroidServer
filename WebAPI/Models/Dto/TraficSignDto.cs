using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class TraficSignDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public TraficSignType Type { get; set; }
    }
}

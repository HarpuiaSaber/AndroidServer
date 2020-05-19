using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Dto
{
    public class RankingDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public int TotalCorrect { get; set; }
        public int Time { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Dto
{
    public class ResultDto
    {
        public long UserId { get; set; }
        public long ExamId { get; set; }
        public int Time { get; set; }
        public int TotalCorrect { get; set; }
    }
}

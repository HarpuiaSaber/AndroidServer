using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class UserResultDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
        public int TotalCorrect { get; set; }
        public ExamType Type { get; set; }
        public String DateAt { get; set; }
    }
}

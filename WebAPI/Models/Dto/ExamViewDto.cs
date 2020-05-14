using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class ExamViewDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
        public ExamType Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

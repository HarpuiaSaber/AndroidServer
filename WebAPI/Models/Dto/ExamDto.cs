using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class ExamDto
    {
        public long Id { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}

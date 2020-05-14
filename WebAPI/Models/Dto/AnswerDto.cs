using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Dto
{
    public class AnswerDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class QuestionDto
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string Explanation { get; set; }
        public QuestionType Type { get; set; }
        public string Image { get; set; }
        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}

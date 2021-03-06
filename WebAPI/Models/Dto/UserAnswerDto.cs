﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Dto
{
    public class UserAnswerDto
    {
        public long UserId { get; set; }
        public long ExamId { get; set; }
        public int Time { get; set; }
        public int TotalCorrect { get; set; }
        public List<Answers> Answers { get; set; }
    }
    public class Answers
    {
        public long QuestionId { get; set; }
        public bool IsRight { get; set; }
    }
}

﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("Exam")]
    public class Exam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Content { get; set; }
        public int Time { get; set; }
        public ExamType Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public enum ExamType
    {
        Theory = 1,
        Trial = 2,
    }
}

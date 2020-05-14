using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
        A1 = 1,
        A2 = 2
    }
}

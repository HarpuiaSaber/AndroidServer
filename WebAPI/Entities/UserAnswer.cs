using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    [Table("UserAnswer")]
    public class UserAnswer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public long ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }   
        public long AnswerId { get; set; }
        [ForeignKey(nameof(AnswerId))]
        public Answer Answer { get; set; }
    }
}

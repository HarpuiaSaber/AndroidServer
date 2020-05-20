using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("Question")]
    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public string Content { get; set; }
        public string Explanation { get; set; }
        public QuestionType Type { get; set; }
    }

    public enum QuestionType
    {
        Law = 1,
        TrafficSign = 2,
        Situation = 3
    }
}

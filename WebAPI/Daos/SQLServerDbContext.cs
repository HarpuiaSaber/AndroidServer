using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Daos
{
    public class SQLServerDbContext : DbContext
    {
        public SQLServerDbContext(DbContextOptions<SQLServerDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<QuestionInExam> QuestionInExams { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<FailQuestion> FailQuestions { get; set; }
        public DbSet<Law> Laws { get; set; }
    }
}

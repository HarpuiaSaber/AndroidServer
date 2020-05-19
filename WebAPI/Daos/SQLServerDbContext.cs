using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public DbSet<WrongQuestion> WrongQuestions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}

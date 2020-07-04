using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Daos;
using WebAPI.Entities;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers.API
{
    [Route("api/question/[action]")]
    [ApiController]
    public class QuestionsAPIController : ControllerBase
    {
        private readonly SQLServerDbContext _context;

        public QuestionsAPIController(SQLServerDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<QuestionDto> GetQuestionById(long id)
        {
            var question = await _context.Questions.Where(s => s.Id == id)
                .Select(s => new QuestionDto
                {
                    Id = s.Id,
                    Content = s.Content,
                    Explanation = s.Explanation,
                    Type = s.Type,
                    Image = s.Image
                }).SingleOrDefaultAsync();
            question.Answers = _context.Answers.Where(s => s.QuestionId == id)
                     .Select(a => new AnswerDto
                     {
                         Id = a.Id,
                         Content = a.Content,
                         IsCorrect = a.IsCorrect
                     });
            return question;
        }
        [HttpGet]
        public async Task<List<WrongQuestionDto>> GetFailQuestions(long userId)
        {
            var userWrongQuestions = _context.FailQuestions.Where(s => s.UserId == userId && !s.Passed);
            var listQuestion = _context.Questions.AsQueryable();
            return await (from q in listQuestion
                          join w in userWrongQuestions on q.Id equals w.QuestionId
                          select new WrongQuestionDto
                          {
                              Id = q.Id,
                              Content = q.Content,
                          }).ToListAsync();
        }
    }
}
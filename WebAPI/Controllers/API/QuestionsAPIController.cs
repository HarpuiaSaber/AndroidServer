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
        public async Task<List<QuestionDto>> GetRandomQuestion()
        {
            var listQuestion = await _context.Questions.OrderBy(s => Guid.NewGuid()).Take(50).ToListAsync();
            var queryAnswer = _context.Answers.AsQueryable();
            return (from q in listQuestion
                    join a in queryAnswer on q.Id equals a.QuestionId into t
                    select new QuestionDto
                    {
                        Id = q.Id,
                        Content = q.Content,
                        Explanation = q.Explanation,
                        Type = q.Type,
                        Answers = t.Select(s => new AnswerDto
                        {
                            Id = s.Id,
                            Content = s.Content,
                            IsCorrect = s.IsCorrect
                        })
                    }).ToList();
        }
        [HttpGet]
        public async Task<List<QuestionDto>> GetQuestionById(long id)
        {
            var listQuestion = _context.Questions.Where(s => s.Id == id);
            var queryAnswer = _context.Answers.AsQueryable();
            return await (from q in listQuestion
                          join a in queryAnswer on q.Id equals a.QuestionId into t
                          select new QuestionDto
                          {
                              Id = q.Id,
                              Content = q.Content,
                              Explanation = q.Explanation,
                              Type = q.Type,
                              Answers = t.Select(s => new AnswerDto
                              {
                                  Id = s.Id,
                                  Content = s.Content,
                                  IsCorrect = s.IsCorrect
                              })
                          }).ToListAsync();
        }
        [HttpGet]
        public async Task<List<WrongQuestionDto>> GetWrongQuestions(long userId)
        {
            var userWrongQuestions = _context.WrongQuestions.Where(s => s.UserId == userId && !s.HasDone);
            var listQuestion = _context.Questions.Where(s => s.Id == userId);
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
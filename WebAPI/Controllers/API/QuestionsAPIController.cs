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
        public async Task<ActionResult<List<QuestionDto>>> GetRandomQuestion()
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
    }
}
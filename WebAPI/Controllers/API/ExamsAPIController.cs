using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Daos;
using WebAPI.Entities;
using WebAPI.Extensions;
using WebAPI.Models;
using WebAPI.Models.Dto;
using WebAPI.Models.Search;
using WebAPI.Paginations;

namespace WebAPI.Controllers.API
{
    [Route("api/Exam/[action]")]
    [ApiController]
    public class ExamsAPIController : ControllerBase
    {
        private readonly SQLServerDbContext _context;

        public ExamsAPIController(SQLServerDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<PaginationResult<ExamViewDto>>> GetPagging(ExamSearch examSearch)
        {
            var query = _context.Exams.AsQueryable();
            if (examSearch.StartDate != null)
            {
                query = query.Where(s => s.CreatedDate >= examSearch.StartDate);
            }
            if (examSearch.EndDate != null)
            {
                query = query.Where(s => s.CreatedDate < examSearch.EndDate);
            }
            if (examSearch.Type != null)
            {
                query = query.Where(s => s.Type > examSearch.Type);
            }
            var queryExam = query.Select(s => new ExamViewDto
            {
                Id = s.Id,
                Content = s.Content,
                CreatedDate = s.CreatedDate,
                Time = s.Time,
                Type = s.Type
            }).OrderBy(s => s.CreatedDate);
            return await QueryableEx.GetPaginationResultAsync(queryExam, examSearch.Param);
        }
        [HttpPost]
        public async Task<ActionResult<ExamDto>> GetById(long id)
        {
            var exam = await _context.Exams.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (exam == null)
            {
                throw new ApplicationException($"Không tồn tại bài thi: {exam.Content}");
            }
            var queryQuestionInExam = _context.QuestionInExams.Where(s => s.ExamId == id);
            var queryQuestion = _context.Questions.AsQueryable();
            var queyAnswer = _context.Answers.AsQueryable();
            var questions = await (from qe in queryQuestionInExam
                                   join q in queryQuestion on qe.QuestionId equals q.Id
                                   join a in queyAnswer on q.Id equals a.QuestionId into t
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
            return new ExamDto
            {
                Id = exam.Id,
                Questions = questions
            };
        }
    }
}

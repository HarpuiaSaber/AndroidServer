using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Daos;
using WebAPI.Entities;
using WebAPI.Models.Dto;
using WebAPI.Models.Search;

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
        public async Task<List<ExamViewDto>> GetAll(ExamSearch examSearch)
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
            return await query.Select(s => new ExamViewDto
            {
                Id = s.Id,
                Content = s.Content,
                CreatedDate = s.CreatedDate,
                Time = s.Time,
                Type = s.Type
            }).ToListAsync();
        }
        [HttpPost]
        public async Task<List<ExamViewDto>> GetAllOfUser(ExamSearch examSearch, long userId)
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
            return await (from e in query
                          join r in _context.Results.Where(s => s.UserId == userId) on e.Id equals r.ExamId into t
                          select new ExamViewDto
                          {
                              Id = e.Id,
                              Content = e.Content,
                              CreatedDate = e.CreatedDate,
                              Time = e.Time,
                              Type = e.Type,
                              UserResult = t.Select(x => x.TotalCorrect).FirstOrDefault()
                          }).ToListAsync();
        }
        [HttpGet]
        public async Task Create(ExamViewDto dto)
        {
            var exam = new Exam
            {
                Content = dto.Content,
                Time = dto.Time,
                Type = dto.Type,
                CreatedDate = DateTime.Now
            };
            await _context.Exams.AddAsync(exam);
            bool examSaved = await _context.SaveChangesAsync() > 0;
            if (examSaved)
            {
                // get questions in 3 type
                var listQuestionLaw = await _context.Questions.Where(s => s.Type == QuestionType.Law)
                    .OrderBy(s => Guid.NewGuid()).Take(10)
                    .Select(s => new QuestionInExam
                    {
                        QuestionId = s.Id,
                        ExamId = exam.Id
                    }).ToListAsync();
                var listQuestionTrafficSign = await _context.Questions.Where(s => s.Type == QuestionType.TrafficSign)
                    .OrderBy(s => Guid.NewGuid()).Take(10)
                    .Select(s => new QuestionInExam
                    {
                        QuestionId = s.Id,
                        ExamId = exam.Id
                    }).ToListAsync();
                var listQuestionSituation = await _context.Questions.Where(s => s.Type == QuestionType.Situation)
                    .OrderBy(s => Guid.NewGuid()).Take(10)
                    .Select(s => new QuestionInExam
                    {
                        QuestionId = s.Id,
                        ExamId = exam.Id
                    }).ToListAsync();
                await _context.QuestionInExams.AddRangeAsync(listQuestionLaw.Concat(listQuestionTrafficSign
                    .Concat(listQuestionSituation)));
                await _context.SaveChangesAsync();
            }
        }
        [HttpPost]
        public async Task<ActionResult<ExamDto>> Get(long id)
        {
            var exam = await _context.Exams.Where(s => s.Id == id).FirstOrDefaultAsync();
            if (exam == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Không tồn tại: {exam.Content}!!!");
            }
            var queryQuestionInExam = _context.QuestionInExams.Where(s => s.ExamId == id);
            var queryQuestion = _context.Questions.AsQueryable();
            var queyAnswer = _context.Answers.AsQueryable();
            var questions = from qe in queryQuestionInExam
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
                            };
            return new ExamDto
            {
                Id = exam.Id,
                Questions = await questions.ToListAsync()
            };
        }
        [HttpPost]
        public async Task SaveResult(UserAnswerDto dto)
        {
            //save result
            Result result = new Result
            {
                ExamId = dto.ExamId,
                UserId = dto.UserId,
                Time = dto.Time,
                TotalCorrect = dto.TotalCorrect
            };
            await _context.Results.AddAsync(result);
            await _context.SaveChangesAsync();
            //get old fail
            var failQuestions = await _context.FailQuestions.Where(s => s.UserId == dto.UserId).ToListAsync();
            var failQuestionIds = failQuestions.Select(s => s.QuestionId);
            //add new fail
            var newFails = dto.Answers.Where(s => !s.Value).Select(s => s.Key);
            var needToAdd = newFails.Except(failQuestionIds)
                .Select(s => new FailQuestion
                {
                    QuestionId = s,
                    UserId = dto.UserId,
                    Times = 1,
                    Passed = false
                });
            await _context.FailQuestions.AddRangeAsync(needToAdd);
            //update times fail
            var increaseTimesIds = failQuestionIds.Intersect(newFails);
            var increaseTimes = failQuestions.Where(s => increaseTimesIds.Contains(s.Id)).Select(s => s);
            foreach (var old in increaseTimes)
            {
                old.Times += 1;
            }
            _context.FailQuestions.UpdateRange(increaseTimes);
            //update passed
            var newCorrects = dto.Answers.Where(s => s.Value).Select(s => s.Key);
            var passed = failQuestions.Where(s => newCorrects.Contains(s.QuestionId)).Select(s => s);
            foreach (var old in passed)
            {
                old.Passed = true;
            }
            _context.FailQuestions.UpdateRange(passed);
            await _context.SaveChangesAsync();
        }
        [HttpGet]
        public async Task<List<ResultDto>> Ranking(long id)
        {
            return await _context.Results.Where(s => s.ExamId == id)
                .OrderByDescending(s => s.TotalCorrect).ThenByDescending(s => s.Time)
                .Select(s => new ResultDto
                {
                    UserId = s.UserId,
                    UserName = s.User.UserName,
                    TotalCorrect = s.TotalCorrect,
                    Time = s.Time
                }).ToListAsync();
        }
    }
}

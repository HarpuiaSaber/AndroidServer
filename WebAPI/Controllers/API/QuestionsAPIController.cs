﻿using System;
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
            var listQuestionType1 = _context.Questions.Where(s => s.Type == QuestionType.Law)
                .OrderBy(s => Guid.NewGuid()).Take(10);
            var listQuestionType2 = _context.Questions.Where(s => s.Type == QuestionType.TrafficSign)
                .OrderBy(s => Guid.NewGuid()).Take(5);
            var listQuestionType3 = _context.Questions.Where(s => s.Type == QuestionType.Situation)
                .OrderBy(s => Guid.NewGuid()).Take(5);
            var queryAnswer = _context.Answers.AsQueryable();
            return await (from q in listQuestionType1
                          join a in queryAnswer on q.Id equals a.QuestionId into t
                          select new QuestionDto
                          {
                              Id = q.Id,
                              Content = q.Content,
                              Explanation = q.Explanation,
                              Type = q.Type,
                              Image = q.Image,
                              Answers = t.Select(s => new AnswerDto
                              {
                                  Id = s.Id,
                                  Content = s.Content,
                                  IsCorrect = s.IsCorrect
                              })
                          }).Concat(from q in listQuestionType2
                                    join a in queryAnswer on q.Id equals a.QuestionId into t
                                    select new QuestionDto
                                    {
                                        Id = q.Id,
                                        Content = q.Content,
                                        Explanation = q.Explanation,
                                        Type = q.Type,
                                        Image = q.Image,
                                        Answers = t.Select(s => new AnswerDto
                                        {
                                            Id = s.Id,
                                            Content = s.Content,
                                            IsCorrect = s.IsCorrect
                                        })
                                    }).Concat(from q in listQuestionType3
                                              join a in queryAnswer on q.Id equals a.QuestionId into t
                                              select new QuestionDto
                                              {
                                                  Id = q.Id,
                                                  Content = q.Content,
                                                  Explanation = q.Explanation,
                                                  Type = q.Type,
                                                  Image = q.Image,
                                                  Answers = t.Select(s => new AnswerDto
                                                  {
                                                      Id = s.Id,
                                                      Content = s.Content,
                                                      IsCorrect = s.IsCorrect
                                                  })
                                              }).ToListAsync();
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
                              Image = q.Image,
                              Answers = t.Select(s => new AnswerDto
                              {
                                  Id = s.Id,
                                  Content = s.Content,
                                  IsCorrect = s.IsCorrect
                              })
                          }).ToListAsync();
        }
        [HttpGet]
        public async Task<List<WrongQuestionDto>> GetFailQuestions(long userId)
        {
            var userWrongQuestions = _context.FailQuestions.Where(s => s.UserId == userId && !s.Passed);
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
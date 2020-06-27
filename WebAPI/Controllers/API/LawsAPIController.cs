using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Daos;
using WebAPI.Entities;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers.API
{
    [ApiController]
    [Route("api/Law/[action]")]
    public class LawsAPIController : ControllerBase
    {
        private readonly SQLServerDbContext _context;
        public LawsAPIController(SQLServerDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpPost]
        public async Task<ActionResult> Create(LawDto input)
        {
            var isExist = await _context.Laws.AnyAsync(s => s.Content == input.Content);
            if (isExist)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Đã tồn tại content: {input.Content}!!!");
            }
            Law law = new Law
            {
               Id = input.Id,
               Content = input.Content,
               Punishment = input.Punishment,
               Type = input.Type
            };
            await _context.AddAsync(law);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<List<LawDto>> GetAll()
        {
            return await _context.Laws
                .Select(s => new LawDto
                {
                   Id = s.Id,
                   Content = s.Content,
                   Punishment = s.Punishment,
                   Type = s.Type
                }).ToListAsync();
        }

        [HttpGet]
        public async Task<List<LawDto>> GetLaw(LawType lawType)
        {
            return await _context.Laws
                .Where(s => s.Type == lawType)
                .Select(s => new LawDto
                {
                    Id = s.Id,
                    Content = s.Content,
                    Punishment = s.Punishment,
                    Type = s.Type
                }).ToListAsync();
        }
    }
}

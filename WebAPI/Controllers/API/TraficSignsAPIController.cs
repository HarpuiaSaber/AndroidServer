using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Daos;
using WebAPI.Entities;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers.API
{
    [ApiController]
    [Route("api/TraficSign/[action]")]
    public class TraficSignsAPIController
    {
        private readonly SQLServerDbContext _context;
        public TraficSignsAPIController(SQLServerDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public async Task<List<TraficSignDto>> Get(TraficSignType traficSignType)
        {
            return await _context.TraficSigns
                .Where(s => s.Type == traficSignType)
                .Select(s => new TraficSignDto
                {
                    Id = s.Id,
                    Content = s.Content,
                    Image = s.Image,
                    Name = s.Name,
                    Type = s.Type
                }).ToListAsync();
        }
    }
}

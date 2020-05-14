using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Daos;
using WebAPI.Models;
using WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using WebAPI.Models.Dto;

namespace WebAPI.Controllers.API
{
    [ApiController]
    [Route("api/User/[action]")]
    public class UsersAPIController : ControllerBase
    {
        private readonly SQLServerDbContext _context;
        public UsersAPIController(SQLServerDbContext dbContext)
        {
            _context = dbContext;
        }
        [HttpGet]
        public async Task<List<UserDto>> GetAll()
        {
            return await _context.Users
                .Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Password = s.Password,
                    UserName = s.UserName,
                    IsActive = s.IsActive
                }).ToListAsync();
        }
        [HttpGet]
        public async Task<List<UserDto>> GetById(long id)
        {
            return await _context.Users.Where(s => s.Id == id)
                .Select(s => new UserDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Password = s.Password,
                    UserName = s.UserName,
                    IsActive = s.IsActive
                }).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create(UserDto dto)
        {
            var isExist = await _context.Users.AnyAsync(s => s.UserName == dto.UserName);
            if (isExist)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Đã tồn tại username: {dto.UserName}!!!");
            }
            User user = new User
            {
                Name = dto.Name,
                Password = dto.Password,
                UserName = dto.UserName,
                IsActive = true
            };
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(long id, string oldPassword, string newPassword)
        {
            User old = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (old == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Không tồn tại user: {old.UserName}!!!");
            }
            if (old.IsActive == false)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"User: {old.UserName} đang bị khóa!!!");
            }
            if (old.Password != oldPassword)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Không đúng mật khẩu cũ!!!");
            }
            old.Password = newPassword;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(UserDto dto)
        {
            var old = await _context.Users.FirstOrDefaultAsync(s => s.Id == dto.Id);
            if (old == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Không tồn tại user: {old.UserName}!!!");
            }
            if (old.IsActive == false)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"User: {old.UserName} đang bị khóa!!!");
            }
            old.Name = dto.Name;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.UserName == userName);
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Không tồn tại user: {userName}!!!");
            }
            if (user.IsActive == false)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"User: {userName} đang bị khóa!!!");
            }
            if (user.Password != password)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Sai mật khẩu!!!");
            }
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            };
        }
        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await Task.Run(() =>
            {

            });
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> Deactive(long id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (user == null)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Không tồn tại user: {user.UserName}!!!");
            }
            user.IsActive = false;
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entity;
using WebAPI.Dao;
using WebAPI.Model;
using System.Net.Http;
using System.Net;
using WebAPI.Exception;

namespace WebAPI.Controllers.API
{
    [ApiController]
    [Route("api/user/[action]")]
    public class UserAPIController : ControllerBase
    {
        private readonly SQLServerDbContext _SQLServerDbContext;
        public UserAPIController(SQLServerDbContext dbContext)
        {
            _SQLServerDbContext = dbContext;
        }
        [HttpGet]
        public async Task<List<UserDto>> GetAll()
        {
            return await _SQLServerDbContext.Users
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
        public async Task Create(UserDto dto)
        {
            var isExist = await _SQLServerDbContext.Users.AnyAsync(s => s.UserName == dto.UserName);
            if (isExist)
            {
                throw new UserFriendlyException($"Đã tồn tại username: {dto.UserName}!!!");
            }
            User user = new User
            {
                Name = dto.Name,
                Password = dto.Password,
                UserName = dto.UserName,
                IsActive = true
            };
            await _SQLServerDbContext.AddAsync(user);
            await _SQLServerDbContext.SaveChangesAsync();
        }
        [HttpGet]
        public async Task<UserDto> Login(string userName, string password)
        {
            var user = await _SQLServerDbContext.Users.FirstOrDefaultAsync(s => s.UserName == userName);
            if (user == null)
            {
                throw new UserFriendlyException($"Không tồn tại user: {userName}!!!");
            }
            if (user.IsActive == false)
            {
                throw new UserFriendlyException($"User: {userName} đang bị khóa!!!");
            }
            if (user.Password == password)
            {
                throw new UserFriendlyException("Sai mật khẩu!!!");
            }
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            };
        }
        [HttpPost]
        public async Task ChangePassword(long id, string password)
        {
            var old = await _SQLServerDbContext.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (old == null)
            {
                throw new UserFriendlyException($"Không tồn tại user: {old.UserName}!!!");
            }
            if (old.IsActive == false)
            {
                throw new UserFriendlyException($"User: {old.UserName} đang bị khóa!!!");
            }
            old.Password = password;
            await _SQLServerDbContext.SaveChangesAsync();
        }
        [HttpPost]
        public async Task ChangeInformation(UserDto dto)
        {
            var old = await _SQLServerDbContext.Users.FirstOrDefaultAsync(s => s.Id == dto.Id);
            if (old == null)
            {
                throw new UserFriendlyException($"Không tồn tại user: {old.UserName}!!!");
            }
            if (old.IsActive == false)
            {
                throw new UserFriendlyException($"User: {old.UserName} đang bị khóa!!!");
            }
            old.Name = dto.Name;
            await _SQLServerDbContext.SaveChangesAsync();
        }
    }
}
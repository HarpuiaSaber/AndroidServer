using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;

namespace WebAPI.Models.Dto
{
    public class UserDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}

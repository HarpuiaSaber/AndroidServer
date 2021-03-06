﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Dob { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
}

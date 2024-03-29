﻿using System;
using System.ComponentModel.DataAnnotations;

namespace _2_UserAPI.Data.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}

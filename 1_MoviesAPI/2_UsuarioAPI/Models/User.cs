﻿using System;
namespace _2_UserAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }
    }
}

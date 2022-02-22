﻿using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs.Cinema
{
    public class CreateCinemaDTO
    {
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        public int AddressId { get; set; }
    }
}

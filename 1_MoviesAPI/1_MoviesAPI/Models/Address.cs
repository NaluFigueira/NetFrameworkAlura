﻿using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Models
{
    public class Address
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Street { get; set; }

        public string Neighborhood { get; set; }

        public int Number { get; set; }

        public Cinema Cinema { get; set; }
    }
}

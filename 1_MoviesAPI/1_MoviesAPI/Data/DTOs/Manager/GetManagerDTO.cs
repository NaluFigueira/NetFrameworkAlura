﻿using System;
using System.Collections.Generic;
using _1_MoviesAPI.Models;

namespace _1_MoviesAPI.Data.DTOs
{
    public class GetManagerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public object Cinemas { get; set; }
    }
}

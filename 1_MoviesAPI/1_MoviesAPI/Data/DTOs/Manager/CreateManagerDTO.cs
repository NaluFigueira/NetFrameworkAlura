using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs.Manager
{
    public class CreateManagerDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs
{
    public class CreateManagerDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

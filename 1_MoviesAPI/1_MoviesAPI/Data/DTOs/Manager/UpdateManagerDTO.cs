using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs
{
    public class UpdateManagerDTO
    {
        [Required]
        public string Name { get; set; }
    }
}

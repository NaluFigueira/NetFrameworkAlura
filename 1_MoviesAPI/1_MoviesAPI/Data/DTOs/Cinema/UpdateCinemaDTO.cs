using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs
{
    public class UpdateCinemaDTO
    {
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
    }
}

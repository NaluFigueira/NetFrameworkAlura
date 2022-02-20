using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
    }
}

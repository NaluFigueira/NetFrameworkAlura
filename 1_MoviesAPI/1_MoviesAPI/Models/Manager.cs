using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Models
{
    public class Manager
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

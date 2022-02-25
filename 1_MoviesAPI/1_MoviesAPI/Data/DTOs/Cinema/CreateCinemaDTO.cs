using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs
{
    public class CreateCinemaDTO
    {
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
        public int AddressId { get; set; }
        public int ManagerId { get; set; }
    }
}

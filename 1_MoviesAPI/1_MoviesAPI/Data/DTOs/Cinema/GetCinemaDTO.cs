using System.Text.Json.Serialization;
using _1_MoviesAPI.Models;

namespace _1_MoviesAPI.Data.DTOs
{
    public class GetCinemaDTO
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public Manager Manager { get; set; }
    }
}

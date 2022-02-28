using System;
using System.Text.Json.Serialization;
using _1_MoviesAPI.Models;
using MoviesAPI.Models;

namespace _1_MoviesAPI.Data.DTOs
{
    public class GetSessionDTO
    {
        public int Id { get; set; }

        public Cinema Cinema { get; set; }

        public Movie Movie { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}

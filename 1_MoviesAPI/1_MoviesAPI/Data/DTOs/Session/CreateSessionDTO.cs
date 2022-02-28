using System;
using System.ComponentModel.DataAnnotations;

namespace _1_MoviesAPI.Data.DTOs
{
    public class CreateSessionDTO
    {
        public int CinemaID { get; set; }

        public int MovieID { get; set; }

        public DateTime StartTime { get; set; }
    }
}

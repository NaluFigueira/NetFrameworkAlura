using System;
using System.ComponentModel.DataAnnotations;
using MoviesAPI.Models;

namespace _1_MoviesAPI.Models
{
    public class Session
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public virtual Cinema Cinema { get; set; }
        public int CinemaID { get; set; }

        public virtual Movie Movie { get; set; }
        public int MovieID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using _1_MoviesAPI.Models;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public enum MovieGenre
        {
            Action,
            Comedy,
            Drama,
            Musical,
            Thriller,
            Horror
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director field is required")]
        public string Director { get; set; }

        [EnumDataType(typeof(MovieGenre), ErrorMessage = "Movie genre value must be between 0 and 6")]
        public MovieGenre Genre { get; set; }

        [Range(60, 300, ErrorMessage = "Duration must be between 60 and 300 minutes")]
        public int Duration { get; set; }

        [JsonIgnore]
        public virtual List<Session> Sessions { get; set; }

    }
}

using System;
using System.Collections.Generic;
using MoviesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>();
        public static int nextMovieID = 1;

        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            movie.Id = nextMovieID++;
            movies.Add(movie);
            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            Movie movie = movies.FirstOrDefault(movie => movie.Id == id);

            if(movie != null)
            {
                return Ok(movie);
            }
            return NotFound();
        }
    }
}
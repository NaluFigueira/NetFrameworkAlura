using System;
using Microsoft.AspNetCore.Mvc;
using static MoviesAPI.Models.Movie;

namespace _1_MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieGenreController :  ControllerBase
    {
        [HttpGet]
        public IActionResult GetMovieGenre()
        {
            return Ok(Enum.GetNames(typeof(MovieGenre)));
        }
    }
}

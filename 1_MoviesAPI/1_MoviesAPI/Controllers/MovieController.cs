using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using _1_MoviesAPI.Data.DTOs;
using static MoviesAPI.Models.Movie;
using _1_MoviesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private MovieService _service;

        public MovieController(MovieService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular")]
        public IActionResult GetMovies([FromQuery] MovieGenre? genre = null)
        {
            List<GetMovieDTO> foundMovies = _service.GetMovies(genre);

            if (foundMovies != null) return Ok(foundMovies);

            return NotFound();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular")]
        public IActionResult GetMovieById(int id)
        {
            GetMovieDTO foundMovie = _service.GetMovieById(id);

            if (foundMovie != null) return Ok(foundMovie);

            return NotFound();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
        {
            GetMovieDTO createdMovie = _service.CreateMovie(createMovieDTO);
            
            return CreatedAtAction(nameof(GetMovieById), new { Id = createdMovie.Id }, createdMovie);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
        {
            Result result = _service.UpdateMovie(id, updateMovieDTO);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            Result result = _service.DeleteMovie(id);

            if (result.IsSuccess) return NoContent();

            return NotFound();
            
        }
    }
}
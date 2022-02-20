using System;
using System.Collections.Generic;
using MoviesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using AutoMapper;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public MovieController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies;
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie != null)
            {
                GetMovieDTO getMovieDTO = _mapper.Map<GetMovieDTO>(movie);
                return Ok(getMovieDTO);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
        {
            Movie newMovie = _mapper.Map<Movie>(createMovieDTO);

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovieById), new { Id = newMovie.Id }, newMovie);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
        {
            Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie != null)
            {
                _mapper.Map(updateMovieDTO, movie);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            Movie movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie != null)
            {
                _context.Remove(movie);
                _context.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
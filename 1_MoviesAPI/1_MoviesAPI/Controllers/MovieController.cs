using System;
using System.Collections.Generic;
using MoviesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using AutoMapper;
using static MoviesAPI.Models.Movie;

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
        public IActionResult GetMovies([FromQuery] MovieGenre? genre = null)
        {
            List<Movie> movies;

            if(genre != null)
            {
                movies = _context.Movies.Where(movie => movie.Genre == genre).ToList();
            }
            else
            {
                movies = _context.Movies.ToList();
            }

            if(movies != null)
            {
                List<GetMovieDTO> getMovieDTOs = _mapper.Map<List<GetMovieDTO>>(movies);

                return Ok(getMovieDTOs);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

            if (movie != null)
            {
                var getMovieDTO = _mapper.Map<GetMovieDTO>(movie);
                return Ok(getMovieDTO);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult CreateMovie([FromBody] CreateMovieDTO createMovieDTO)
        {
            var newMovie = _mapper.Map<Movie>(createMovieDTO);

            _context.Movies.Add(newMovie);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMovieById), new { Id = newMovie.Id }, newMovie);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDTO)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

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
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);

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
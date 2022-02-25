using System;
using System.Linq;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1_MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public CinemaController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCinemas()
        {
            return Ok(_context.Cinemas);
        }

        [HttpGet("{id}")]
        public IActionResult GetCinemaById(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema != null)
            {
                var getCinemaDTO = _mapper.Map<GetCinemaDTO>(cinema);
                return Ok(getCinemaDTO);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateCinema([FromBody] CreateCinemaDTO createCinemaDTO)
        {

            var newCinema = _mapper.Map<Cinema>(createCinemaDTO);
            _context.Cinemas.Add(newCinema);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCinemaById), new { Id = newCinema.Id }, newCinema );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDTO updateCinemaDTO)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if(cinema != null)
            {
                _mapper.Map(updateCinemaDTO, cinema);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(cinema => cinema.Id == id);

            if (cinema != null)
            {
                _context.Remove(cinema);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}

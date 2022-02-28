using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1_MoviesAPI.Data;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1_MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public SessionController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSessions()
        {
            var sessions = _context.Sessions.ToList();
            return Ok(_mapper.Map<List<GetSessionDTO>>(sessions));
        }

        [HttpGet("{id}")]
        public IActionResult GetSessionById(int id)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if(session != null)
            {
                var getSessionDTO = _mapper.Map<GetSessionDTO>(session);
                return Ok(getSessionDTO);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateSession([FromBody] CreateSessionDTO createSessionDTO)
        {
            var sessionMovie = _context.Movies.FirstOrDefault(movie => movie.Id == createSessionDTO.MovieID);

            if(sessionMovie != null)
            {

                var newSession = _mapper.Map<Session>(createSessionDTO);
                newSession.EndTime = newSession.StartTime.AddMinutes(sessionMovie.Duration);
                _context.Sessions.Add(newSession);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetSessionById), new { Id = newSession.Id }, newSession);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDTO updateSessionDTO)
        {
            var session = _context.Sessions.FirstOrDefault(session => session.Id == id);

            if(session != null)
            {
                _mapper.Map(updateSessionDTO, session);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSession(int id)
        {
            var session = _context.Cinemas.FirstOrDefault(session => session.Id == id);

            if (session != null)
            {
                _context.Remove(session);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}

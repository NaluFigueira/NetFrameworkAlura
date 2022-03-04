using System;
using System.Collections.Generic;
using _1_MoviesAPI.Data.DTOs;
using _1_MoviesAPI.Models;
using _1_MoviesAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace _1_MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private SessionService _service;

        public SessionController(SessionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetSessions()
        {
            List<GetSessionDTO> foundSessions = _service.GetSessions();

            if(foundSessions != null) return Ok(foundSessions);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetSessionById(int id)
        {
            GetSessionDTO foundSession = _service.GetSessionById(id);

            if (foundSession != null) return Ok(foundSession);

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateSession([FromBody] CreateSessionDTO createSessionDTO)
        {
            Session createdSession = _service.CreateSession(createSessionDTO);

            if(createdSession != null)
            {
                return CreatedAtAction(nameof(GetSessionById), new { Id = createdSession.Id }, createdSession);
            }

            return NotFound(new NotFoundObjectResult( new { Error = "Movie ID or cinema ID not found" }));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSession(int id, [FromBody] UpdateSessionDTO updateSessionDTO)
        {
            Result result = _service.UpdateSession(id, updateSessionDTO);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSession(int id)
        {
            Result result = _service.DeleteSession(id);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }
    }
}

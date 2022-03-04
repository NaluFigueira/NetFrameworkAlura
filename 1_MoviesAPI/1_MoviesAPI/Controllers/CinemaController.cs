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
    public class CinemaController : ControllerBase
    {
        private CinemaService _service;

        public CinemaController(CinemaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCinemas([FromQuery] string movieTitle = null)
        {
            List<GetCinemaDTO> foundCinemas = _service.GetCinemas(movieTitle);

            if(foundCinemas != null) return Ok(foundCinemas);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetCinemaById(int id)
        {
            GetCinemaDTO foundCinema = _service.GetCinemaById(id);

            if(foundCinema != null) return Ok(foundCinema);

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateCinema([FromBody] CreateCinemaDTO createCinemaDTO)
        {
            Cinema createdCinema = _service.CreateCinema(createCinemaDTO);

            return CreatedAtAction(nameof(GetCinemaById), new { Id = createdCinema.Id }, createdCinema);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDTO updateCinemaDTO)
        {
            Result result = _service.UpdateCinema(id, updateCinemaDTO);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            Result result = _service.DeleteCinema(id);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }
    }
}

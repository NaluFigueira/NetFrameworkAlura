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
    public class ManagerController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public ManagerController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            return Ok(_context.Managers);
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager != null)
            {
                var getManagerDTO = _mapper.Map<GetManagerDTO>(manager);

                return Ok(getManagerDTO);
            }

            return NotFound();

        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] CreateManagerDTO createManagerDTO)
        {
            var manager = _mapper.Map<Manager>(createManagerDTO);

            _context.Add(manager);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetManagerById), new { Id = manager.Id }, manager);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(int id, [FromBody] UpdateManagerDTO updateManagerDTO)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager != null)
            {
                _mapper.Map(updateManagerDTO, manager);
                var getManagerDTO = _mapper.Map<GetManagerDTO>(manager);

                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int id)
        {
            var manager = _context.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager != null)
            {
                _context.Remove(manager);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}

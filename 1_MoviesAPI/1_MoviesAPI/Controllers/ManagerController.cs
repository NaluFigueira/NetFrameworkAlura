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
    public class ManagerController : ControllerBase
    {
        private ManagerService _service;

        public ManagerController(ManagerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            List<GetManagerDTO> foundManagers = _service.GetAllManagers();

            if(foundManagers != null) return Ok(foundManagers);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(int id)
        {
            GetManagerDTO foundManager = _service.GetManagerById(id);

            if(foundManager != null) return Ok(foundManager);

            return NotFound();

        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] CreateManagerDTO createManagerDTO)
        {
            Manager createdManager = _service.CreateManager(createManagerDTO);

            return CreatedAtAction(nameof(GetManagerById), new { Id = createdManager.Id }, createdManager);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(int id, [FromBody] UpdateManagerDTO updateManagerDTO)
        {
            Result result = _service.UpdateManager(id, updateManagerDTO);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int id)
        {
            Result result = _service.DeleteManager(id);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }
    }
}

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
    public class AddressController : ControllerBase
    {
        private AddressService _service;

        public AddressController(AddressService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            List<GetAddressDTO> foundAddresses = _service.GetAddresses();

            if(foundAddresses != null) return Ok(foundAddresses);

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            GetAddressDTO foundAddress = _service.GetAddressById(id);

            if(foundAddress != null) return Ok(foundAddress);

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
        {
            Address createdAddress = _service.CreateAddress(createAddressDTO);

            return CreatedAtAction(nameof(GetAddressById), new { Id = createdAddress.Id }, createdAddress);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDTO updateAddressDTO)
        {
            Result result = _service.UpdateAddress(id, updateAddressDTO);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Result result = _service.DeleteAddress(id);

            if (result.IsSuccess) return NoContent();

            return NotFound();
        }
    }
}

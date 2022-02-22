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
    public class AddressController : ControllerBase
    {
        private MovieContext _context;
        private IMapper _mapper;

        public AddressController(MovieContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAddresses()
        {
            return Ok(_context.Addresses);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if(address != null)
            {
                var getAddressDTO = _mapper.Map<GetAddressDTO>(address);
                return Ok(getAddressDTO);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] CreateAddressDTO createAddressDTO)
        {
            var address = _mapper.Map<Address>(createAddressDTO);
            _context.Addresses.Add(address);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int id, [FromBody] UpdateAddressDTO updateAddressDTO)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address != null)
            {
                _mapper.Map(updateAddressDTO, address);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var address = _context.Addresses.FirstOrDefault(address => address.Id == id);

            if (address != null)
            {
                _context.Remove(address);
                _context.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}

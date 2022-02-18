using System;
using System.Linq;
using AutoMapper;
using BookList.Data;
using BookList.Data.DTOs;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase
    {
        private BookContext _context;
        private IMapper _mapper;

        public ListController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateList([FromBody] CreateListDTO listDTO)
        {
            var list = _mapper.Map<List>(listDTO);
            _context.Lists.Add(list);
            _context.SaveChanges();
            return Created(new Uri($"http://localhost:5000/Book/{list.Id}"), list);
        }

        [HttpPut("{id}")]
        public IActionResult AddBookToList(int id, [FromBody] UpdateListDTO updateListDTO)
        {
            var list = _context.Lists.FirstOrDefault(list => list.Id == id);

            if(list == null)
            {
                return NotFound();
            }

            _mapper.Map(updateListDTO, list);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetAllLists()
        {
            if (_context.Lists.Any())
            {
                return Ok(_context.Lists);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetList(int id)
        {
            var list = _context.Lists.FirstOrDefault(list => list.Id == id);

            if (list != null)
            {
                return Ok(list);
            }

            return NotFound();
        }
    }
}

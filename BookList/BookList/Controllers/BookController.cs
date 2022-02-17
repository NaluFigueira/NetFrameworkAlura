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
    public class BookController : ControllerBase
    {
        private BookContext _context;
        private IMapper _mapper;

        public BookController(BookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);

            _context.Add(book);
            _context.SaveChanges();
            return Created(new Uri($"http://localhost:5000/Book/{book.Id}"), book);
        }

        [HttpGet]
        public IActionResult GetAllBooks(uint id)
        {
            if (_context.Books.Any())
            {
                return Ok(_context.Books);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == id);

            if (book != null)
            {
                return Ok(book);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookDTO bookDTO)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookDTO, book);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(book => book.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Remove(book);

            _context.SaveChanges();

            return NoContent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookList.Models;

namespace BookList.Data.DTOs
{
    public class CreateListDTO
    {
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        [Required]
        public IEnumerable<Book> Books;
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace BookList.Data.DTOs
{
    public class UpdateBookDTO
    {
        
        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        public string Subtitle { get; set; }

        [Required(ErrorMessage = "Author field is required")]
        [MinLength(6, ErrorMessage = "Author name should be at least 6 chracters long")]
        public string Author { get; set; }

        public string Synopsis { get; set; }

        public byte[] Cover { get; set; }
    }
}

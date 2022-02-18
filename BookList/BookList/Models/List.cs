using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookList.Models
{
    public class List
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public List()
        {
        }
    }
}

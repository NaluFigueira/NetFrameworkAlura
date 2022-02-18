using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookList.Models
{
    public class List
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title field is required")]
        public string Title { get; set; }

        [ForeignKey("ListId")]
        public virtual ICollection<Book> Books { get; set; }

        public List()
        {
        }
    }
}

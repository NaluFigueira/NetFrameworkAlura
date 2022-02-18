using System;
using BookList.Models;
using Microsoft.EntityFrameworkCore;

namespace BookList.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> opt) : base(opt)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<List> Lists { get; set; }
    }
}

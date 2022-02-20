using System;
using _1_MoviesAPI.Models;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;

namespace _1_MoviesAPI.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> opt) : base(opt)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
    }
}

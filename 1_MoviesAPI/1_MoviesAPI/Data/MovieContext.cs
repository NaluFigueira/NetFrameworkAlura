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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Address)
                .HasForeignKey<Cinema>(cinema => cinema.AddressId);

            builder.Entity<Cinema>()
                .HasOne(cinema => cinema.Manager)
                .WithMany(manager => manager.Cinemas)
                .HasForeignKey(cinema => cinema.ManagerId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}

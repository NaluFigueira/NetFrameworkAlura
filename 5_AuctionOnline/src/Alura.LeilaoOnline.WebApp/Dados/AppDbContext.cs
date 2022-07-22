using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public class AppDbContext : DbContext
    {
        public DbSet<Leilao> Leiloes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public IConfiguration Configuration { get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .AddUserSecrets<AppDbContext>()
               .Build();
            string stringconexao = builder.GetConnectionString("AuctionOnlineConnection");
            optionsBuilder.UseMySQL(stringconexao);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leilao>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Leiloes)
                .HasForeignKey(l => l.IdCategoria);
        }
    }
}
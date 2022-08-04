using System;
using System.Collections.Generic;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace Alura.LeilaoOnline.WebApp.Dados.EFCore
{
    public class LeilaoDAO : ILeilaoDAO
    {
        AppDbContext _context;


        public LeilaoDAO()
        {
            _context = new AppDbContext();
        }

        public List<Categoria> GetCategories()
        {
            return _context.Categorias.ToList();
        }

        public List<Leilao> GetAuctions()
        {
            return _context.Leiloes.Include(l => l.Categoria).ToList();
        }

        public Leilao FindAuctionById(int id)
        {
            return _context.Leiloes.First(auction => auction.Id == id);
        }

        public void Add(Leilao auction)
        {
            _context.Leiloes.Add(auction);
            _context.SaveChanges();
        }

        public void Update(Leilao auction)
        {
            _context.Leiloes.Update(auction);
            _context.SaveChanges();
        }

        public void Delete(Leilao auction)
        {
            _context.Leiloes.Remove(auction);
            _context.SaveChanges();
        }

    }
}

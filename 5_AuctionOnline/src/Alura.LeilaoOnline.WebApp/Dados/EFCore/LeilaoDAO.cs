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


        public IEnumerable<Leilao> GetAll()
        {
            return _context.Leiloes.Include(l => l.Categoria).ToList();
        }

        public Leilao GetById(int id)
        {
            return _context.Leiloes.First(auction => auction.Id == id);
        }

        public IEnumerable<Leilao> GetAuctionsByTerm(string term)
        {
            return _context.Leiloes
                .Where(c =>
                    c.Titulo.ToUpper().Contains(term) ||
                    c.Descricao.ToUpper().Contains(term) ||
                    c.Categoria.Descricao.ToUpper().Contains(term));
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

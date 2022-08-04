using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ILeilaoDAO
    {
        public List<Categoria> GetCategories();

        public List<Leilao> GetAuctions();

        public Leilao FindAuctionById(int id);

        public void Add(Leilao auction);

        public void Update(Leilao auction);

        public void Delete(Leilao auction);

    }
}

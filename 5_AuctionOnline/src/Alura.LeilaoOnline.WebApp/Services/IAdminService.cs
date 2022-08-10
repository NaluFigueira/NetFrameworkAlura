using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services
{
    public interface IAdminService
    {
        public IEnumerable<Categoria> GetCategories();

        public IEnumerable<Leilao> GetAuctions();

        public Leilao FindAuctionById(int id);

        public void Add(Leilao auction);

        public void Update(Leilao auction);

        public bool CheckIfAuctionIsDeletable(Leilao auction);

        public void Delete(Leilao auction);

        public void StartAuctionUsingId(int id);

        public void FinishAuctionUsingId(int id);
    }
}

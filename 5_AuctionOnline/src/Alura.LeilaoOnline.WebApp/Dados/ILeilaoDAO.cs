using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ILeilaoDAO : IQuery<Leilao>, ICommand<Leilao>
    {
        public IEnumerable<Leilao> GetAuctionsByTerm(string term);
    }
}

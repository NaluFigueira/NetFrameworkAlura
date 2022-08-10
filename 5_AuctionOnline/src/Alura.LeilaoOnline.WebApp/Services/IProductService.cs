using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services
{
    public interface IProductService
    {
        IEnumerable<Leilao> GetAuctionsByTerm(string term);
        IEnumerable<Categoria> GetCategoriesWithAuctionsTotal();
        Categoria GetCategoriesByIdWithOpenAuctions(int id);
    }
}

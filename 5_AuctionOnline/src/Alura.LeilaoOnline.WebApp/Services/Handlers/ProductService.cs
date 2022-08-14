using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class ProductService : IProductService
    {
        ILeilaoDAO _leilaoDao;
        ICategoriaDAO _categoriaDao;

        public ProductService(ILeilaoDAO leilaoDao, ICategoriaDAO categoriaDao)
        {
            _leilaoDao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public Categoria GetCategoriesByIdWithOpenAuctions(int id)
        {
            return _categoriaDao.GetById(id);
        }

        public IEnumerable<Categoria> GetCategoriesWithAuctionsTotal()
        {
            return _categoriaDao.GetCategoriesWithAuctionsTotal();
        }

        public IEnumerable<Leilao> GetAuctionsByTerm(string term)
        {
            return _leilaoDao.GetAuctionsByTerm(term);
        }
    }
}

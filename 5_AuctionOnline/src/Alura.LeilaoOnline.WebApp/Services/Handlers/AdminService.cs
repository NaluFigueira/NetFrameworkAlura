using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class AdminService : IAdminService
    {
        ILeilaoDAO _leilaoDao;
        ICategoriaDAO _categoriaDao;

        public AdminService(ILeilaoDAO leilaoDao, ICategoriaDAO categoriaDao)
        {
            _leilaoDao = leilaoDao;
            _categoriaDao = categoriaDao;
        }

        public IEnumerable<Categoria> GetCategories()
        {
            return _categoriaDao.GetCategories();
        }

        public IEnumerable<Leilao> GetAuctions()
        {
            return _leilaoDao.GetAuctions();
        }

        public Leilao FindAuctionById(int id)
        {
            return _leilaoDao.FindAuctionById(id);
        }

        public void Add(Leilao auction)
        {
            _leilaoDao.Add(auction);
        }

        public void Update(Leilao auction)
        {
            _leilaoDao.Update(auction);
        }

        public bool CheckIfAuctionIsDeletable(Leilao auction)
        {
            return auction != null && auction.Situacao != SituacaoLeilao.Pregao;
        }

        public void Delete(Leilao auction)
        {
            if(CheckIfAuctionIsDeletable(auction))
            {
                _leilaoDao.Delete(auction);
            }
        }

        public void StartAuctionUsingId(int id)
        {
            var leilao = _leilaoDao.FindAuctionById(id);
            if (leilao.Situacao == SituacaoLeilao.Rascunho)
            {
                leilao.Situacao = SituacaoLeilao.Pregao;
                leilao.Inicio = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }

        public void FinishAuctionUsingId(int id)
        {
            var leilao = _leilaoDao.FindAuctionById(id);
            if (leilao != null && leilao.Situacao == SituacaoLeilao.Pregao)
            {
                leilao.Situacao = SituacaoLeilao.Finalizado;
                leilao.Termino = DateTime.Now;
                _leilaoDao.Update(leilao);
            }
        }
    }
}

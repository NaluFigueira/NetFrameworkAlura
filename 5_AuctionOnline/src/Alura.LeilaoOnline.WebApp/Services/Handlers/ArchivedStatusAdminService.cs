using System;
using System.Collections.Generic;
using System.Linq;
using Alura.LeilaoOnline.WebApp.Dados;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Services.Handlers
{
    public class ArchivedStatusAdminService : IAdminService
    {
        IAdminService _service;

        public ArchivedStatusAdminService(ILeilaoDAO leilaoDao, ICategoriaDAO categoriaDao)
        {
            _service = new AdminService(leilaoDao, categoriaDao);
        }

        public bool CheckIfAuctionIsDeletable(Leilao auction)
        {
            return _service.CheckIfAuctionIsDeletable(auction);
        }

        void IAdminService.Add(Leilao auction)
        {
            _service.Add(auction);
        }

        void IAdminService.Delete(Leilao auction)
        {
            if (CheckIfAuctionIsDeletable(auction))
            {
                auction.Situacao = SituacaoLeilao.Arquivado;
                _service.Update(auction);
            }
        }

        Leilao IAdminService.FindAuctionById(int id)
        {
            return _service.FindAuctionById(id);
        }

        void IAdminService.FinishAuctionUsingId(int id)
        {
            _service.FinishAuctionUsingId(id);
        }

        IEnumerable<Leilao> IAdminService.GetAuctions()
        {
            return _service.GetAuctions()
                .Where(auction => auction.Situacao != SituacaoLeilao.Arquivado);
        }

        IEnumerable<Categoria> IAdminService.GetCategories()
        {
            return _service.GetCategories();
        }

        void IAdminService.StartAuctionUsingId(int id)
        {
            _service.StartAuctionUsingId(id);
        }

        void IAdminService.Update(Leilao auction)
        {
            _service.Update(auction);
        }
    }
}

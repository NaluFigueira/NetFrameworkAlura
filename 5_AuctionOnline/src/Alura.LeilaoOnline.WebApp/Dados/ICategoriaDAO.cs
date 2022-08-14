using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ICategoriaDAO : IQuery<Categoria>
    {

        public IEnumerable<Categoria> GetCategoriesWithAuctionsTotal();
    }
}

using System;
using System.Collections.Generic;
using Alura.LeilaoOnline.WebApp.Models;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ICategoriaDAO
    {

        public List<Categoria> GetCategories();
        public Categoria GetCategoryById(int id);
        public IEnumerable<Categoria> GetCategoriesWithAuctionsTotal();
    }
}

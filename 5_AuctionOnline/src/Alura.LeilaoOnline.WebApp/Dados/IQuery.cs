using System;
using System.Collections;
using System.Collections.Generic;

namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface IQuery<T>
    {

        public IEnumerable<T> GetAll();

        public T GetById(int id);
    }
}

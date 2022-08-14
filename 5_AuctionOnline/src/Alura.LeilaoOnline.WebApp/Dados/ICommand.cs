using System;
namespace Alura.LeilaoOnline.WebApp.Dados
{
    public interface ICommand<T>
    {
        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);
    }
}

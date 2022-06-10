using System;
using System.Collections.Generic;
using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repositorio;
        public ClienteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IClienteRepositorio>();

        }

        [Fact]
        public void TestaObterTodosClientes()
        {
            //Arrange

            //Act
            List<Cliente> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotEmpty(lista);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterContaPeloId(int id)
        {
            //Arrange

            //Act
            Cliente cliente = _repositorio.ObterPorId(id);

            //Assert
            Assert.Equal(id, cliente.Id);
        }
    }
}

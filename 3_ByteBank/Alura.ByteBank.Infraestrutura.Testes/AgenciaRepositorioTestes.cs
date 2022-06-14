using System;
using System.Collections.Generic;
using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Dominio.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repositorio;

        public AgenciaRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IAgenciaRepositorio>();

        }

        [Fact]
        public void TestaObterTodasAgencias()
        {
            //Arrange

            //Act
            List<Agencia> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotEmpty(lista);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciaPeloId(int id)
        {
            //Arrange

            //Act
            Agencia conta = _repositorio.ObterPorId(id);

            //Assert
            Assert.Equal(id, conta.Id);
        }

        [Fact]
        public void TestaExcecaoObterAgenciaPeloId()
        {
            //Arrange
            //Assert
            Assert.Throws<Exception>(() =>
            {
                //Act
                _repositorio.ObterPorId(-1);
            });
        }

        [Fact]
        public void TestaAgenciaServicoObterTodos()
        {
            //Arrange
            var agenciaRepositorioMock = new Mock<IAgenciaRepositorio>();
            var mock = agenciaRepositorioMock.Object;
            var service = new AgenciaServico(mock);

            //Act
            var lista = service.ObterTodos();

            //Asssert
            agenciaRepositorioMock.Verify(agenciaRepositorio => agenciaRepositorio.ObterTodos());
        }

        [Fact]
        public void TestaAgenciaServicoObterPorIdInvalido()
        {
            //Arrange
            var agenciaRepositorioMock = new Mock<IAgenciaRepositorio>();
            agenciaRepositorioMock.Setup(x => x.ObterPorId(-1)).Returns<Agencia>(null);
            var mock = agenciaRepositorioMock.Object;
            var service = new AgenciaServico(mock);

            //Act
            var agencia = service.ObterPorId(-1);

            //Asssert
            Assert.Null(agencia);
        }
    }
}

using System;
using System.Collections.Generic;
using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repositorio;

        public ContaCorrenteRepositorioTestes()
        {
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repositorio = provedor.GetService<IContaCorrenteRepositorio>();

        }

        [Fact]
        public void TestaObterTodasContas()
        {
            //Arrange

            //Act
            List<ContaCorrente> lista = _repositorio.ObterTodos();

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
            ContaCorrente conta = _repositorio.ObterPorId(id);

            //Assert
            Assert.Equal(id, conta.Id);
        }

        [Fact]
        public void TestaAtualizaSaldo()
        {
            //Arrange
            var conta = _repositorio.ObterPorId(1);
            double saldoNovo = 150;
            conta.Saldo = saldoNovo;

            //Act
            var atualizadoComSucesso = _repositorio.Atualizar(conta);

            //Assert
            Assert.True(atualizadoComSucesso);
        }


        [Fact]
        public void TestaInsereUmaNovaConta()
        {
            //Arrange
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Id = 1,
                    Nome = "Kent Nelson",
                    CPF = "486.074.980-45",
                    Identificador = Guid.NewGuid(),
                    Profissao = "Bancário",
                },
                Agencia = new Agencia()
                {
                    Id = 1,
                    Nome = "Agencia Central Coast City",
                    Identificador = Guid.NewGuid(),
                    Endereco = "Rua das Flores, 25",
                    Numero = 0123,
                }
            };

            //Act
            var inseriuContaComSucesso = _repositorio.Adicionar(conta);

            //Assert
            Assert.True(inseriuContaComSucesso);
        }
    }
}

using System;
using Alura.ByteBank.Dados.Contexto;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ByteBankContextoTestes
    {
        [Fact]
        public void TestaConexaoContextoComMySQL()
        {
            //Arrange
            var contexto = new ByteBankContexto();
            bool conectado;

            //Act
            try
            {
                conectado = contexto.Database.CanConnect();
            }
            catch
            {
                throw new Exception("Não foi poss[ivel conectar com a base de dados.");
            }

            //Assert
            Assert.True(conectado);
        }

    }
}

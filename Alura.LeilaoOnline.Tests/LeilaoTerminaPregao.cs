using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProxioDadoLeilaoNessaModalidade(
            double valorDestino,
            double valorEsperado,
            double[] ofertas
            )
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh", valorDestino);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

        [Fact]
        public void LancaInvalidOperationDadoPregaoNaoIniciado()
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");

            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                //Act - método sob teste
                () => leilao.TerminaPregao()
            );

            var mensagemEsperada = "Não é permitido finalizar o pregão sem que ele tenha inicializado." +
                "Utilize o método IniciaPregao().";

            Assert.Equal(mensagemEsperada, excecaoObtida.Message);
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");

            leilao.IniciaPregao();
            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 990, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComPeloOuMenosUmLance(
            double valorEsperado,
            double[] ofertas
            )
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if (i % 2 == 0)
                {
                    leilao.RecebeLance(fulano, valor);
                }
                else
                {
                    leilao.RecebeLance(maria, valor);
                }
            }
            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

    }
}

using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTestes
    {
        [Fact]
        public void LeilaoSemLances()
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");


            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void LeilaoComVariosLances(
            double valorEsperado,
            double[] ofertas
            )
        {
            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }


            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

    }
}

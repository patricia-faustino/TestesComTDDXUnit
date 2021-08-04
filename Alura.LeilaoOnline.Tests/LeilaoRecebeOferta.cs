using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 100, 1200, 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(
            int quantidadeEsperada,
            double[] ofertas)
        {

            //Arranje- cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);
            leilao.IniciaPregao();
            foreach (var valor in ofertas)
            {
                leilao.RecebeLance(fulano, valor);
            }

            leilao.TerminaPregao();

            //Act - método sob teste
            leilao.RecebeLance(fulano, 1000);

            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var quantidadeObtida = leilao.Lances.Count();

            Assert.Equal(quantidadeEsperada, quantidadeObtida);


        }
    }
}

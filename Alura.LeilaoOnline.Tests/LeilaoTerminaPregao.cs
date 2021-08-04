﻿using Alura.LeilaoOnline.Core;
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
            leilao.IniciaPregao();

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

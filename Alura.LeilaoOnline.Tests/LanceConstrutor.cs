using System;
using System.Threading.Tasks;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            //Arranje
            var valorNegativo = -100;

            Assert.Throws<ArgumentException>(
                //Act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}

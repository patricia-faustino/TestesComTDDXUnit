using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {
        private static void LeilaoComVariosLances()
        {
            //Arranje- cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);
            leilao.RecebeLance(maria, 1200);
            leilao.RecebeLance(fulano, 990);

            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorEsperado = 1200;
            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

        }

        private static void LeilaoComApenasUmLance()
        {
            //Arranje- cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessada("Fulano", leilao);
            var maria = new Interessada("Maria", leilao);

            leilao.RecebeLance(fulano, 800);


            //Act - método sob teste
            leilao.TerminaPregao();

            //Assert - Verificação das expectativas
            var valorEsperado = 800;
            var valorObtido = leilao.Ganhador.Valor;
            Verifica(valorEsperado, valorObtido);
        }

        private static void Verifica(int valorEsperado, double valorObtido)
        {
            var corConsole = Console.ForegroundColor;

            if (valorEsperado == valorObtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TESTE OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"TESTE FALHOU! ESPERADO: {valorEsperado}." +
                    $"OBTIDO: {valorObtido}");

            }
            Console.ForegroundColor = corConsole;
        }

        static void Main(string[] args)
        {
            LeilaoComVariosLances();

            LeilaoComApenasUmLance();
        }

    }
}

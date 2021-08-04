using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        private Interessada _ultimoCliente;

        private IList<Lance> _lances;
        public double ValorDestino { get; }

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }

        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, double valorDestino = 0)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            ValorDestino = valorDestino;
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (NovoLanceEhAceito(cliente))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }

        }

        private bool NovoLanceEhAceito(Interessada cliente)
        {
            return Estado == EstadoLeilao.LeilaoEmAndamento
                && cliente != _ultimoCliente;
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é permitido finalizar o pregão sem que ele tenha inicializado." +
                "Utilize o método IniciaPregao().");
            }
            if (ValorDestino > 0)
            {
                // modalidade oferta superior mais próxima
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .Where(lance => lance.Valor > ValorDestino)
                    .OrderBy(lance => lance.Valor)
                    .FirstOrDefault();

            }
            else
            {
                Ganhador = Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(lance => lance.Valor)
                    .LastOrDefault();
            }

            Estado = EstadoLeilao.LeilaoFinalizado;

        }
    }
}

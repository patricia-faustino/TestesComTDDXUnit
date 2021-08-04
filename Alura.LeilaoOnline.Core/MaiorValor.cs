using System.Linq;


namespace Alura.LeilaoOnline.Core
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public Lance Avaliacao(Leilao leilao)
        {
            return leilao.Lances
                    .DefaultIfEmpty(new Lance(null, 0))
                    .OrderBy(lance => lance.Valor)
                    .LastOrDefault();
        }
    }
}

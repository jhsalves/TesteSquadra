using SqTec.Spec.Dtos;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqTec.Console.Services
{
    class ExibicaoService : IExibicaoService
    {
        public IEnumerable<RegiaoExibicao> AgruparClientesExibicaoPorRegiao(IEnumerable<ClienteExibicao> clientes)
        {
            return clientes.
                GroupBy(gc => gc.Regiao).
                Select(x => new RegiaoExibicao(x.Key,
                                               x.Sum(re => re.MedalhasOuro),
                                               x.Sum(re => re.MedalhasPrata),
                                               x.Sum(re => re.MedalhasBronze),
                                               x.Sum(re => re.ValorDesconto))
                                               );
        }

        public void ExibirClientes(IEnumerable<ClienteExibicao> clientes)
        {
            var totalWidthNome = clientes.OrderByDescending(x => x.Nome.Length).FirstOrDefault().Nome.Length + 1;
            clientes.OrderBy(o => o.Nome).ToList().ForEach(c =>
            {
                System.Console.WriteLine(String.Format("{0} | {1} anos | Ouro: {2} | Prata: {3} | Bronze: {4} | Desconto: R${5}",
                                                  c.Nome.PadRight(totalWidthNome, ' '),
                                                  c.Idade,
                                                  c.MedalhasOuro.ToString().PadLeft(2, '0'),
                                                  c.MedalhasPrata.ToString().PadLeft(3, '0'),
                                                  c.MedalhasBronze.ToString().PadLeft(4, '0'),
                                                  c.ValorDesconto));
            });
        }

        public void ExibirSumarizadoPorRegiao(IEnumerable<RegiaoExibicao> regioes)
        {
            regioes.OrderBy(o => o.Regiao).ToList().ForEach(r =>
            {
                System.Console.WriteLine(String.Format("{0} | Ouro: {1} | Prata: {2} | Bronze: {3} | Desconto: R${4}",
                                                  r.Regiao,
                                                  r.MedalhasOuro.ToString().PadLeft(2, '0'),
                                                  r.MedalhasPrata.ToString().PadLeft(2, '0'),
                                                  r.MedalhasBronze.ToString().PadLeft(2, '0'),
                                                  r.ValorDesconto));
            });
        }
    }
}

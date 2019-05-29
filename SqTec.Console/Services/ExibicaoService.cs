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
            throw new NotImplementedException();
        }

        public void ExibirClientes(IEnumerable<ClienteExibicao> clientes)
        {
            throw new NotImplementedException();
        }

        public void ExibirSumarizadoPorRegiao(IEnumerable<RegiaoExibicao> regioes)
        {
            throw new NotImplementedException();
        }
    }
}

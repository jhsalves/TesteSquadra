using SqTec.Spec.Entities;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqTec.Console.Services
{
    class PremiacaoService : IPremiacaoService
    {
        private readonly IClienteService _clienteService;
        public PremiacaoService(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        public double CalcularDesconto(ICliente cliente)
        {
            Func<int, int> fatorial = (int numero) =>
            {
                int fact = 1;
                for (int i = 1;  i <= numero; i++)
                {
                    fact *= i;
                }
                return fact;
            };
            var ouros = CalcularMedalhasOuro(cliente);
            var metadeOuros = ouros / 2;
            var combinacao = fatorial(ouros) / (fatorial(metadeOuros) * (fatorial(ouros - metadeOuros)));
            var idade = _clienteService.CalcularIdade(cliente);
            var resultado = combinacao + (2 * idade);
            if(resultado > 3000)
            {
                resultado = 3000;
            }
            return (double)resultado / 100;
        }

        public int CalcularMedalhasBronze(ICliente cliente)
        {
            return cliente.Pontos / 100;
        }

        public int CalcularMedalhasOuro(ICliente cliente)
        {
            return cliente.Pontos / 10000;
        }

        public int CalcularMedalhasPrata(ICliente cliente)
        {
            return cliente.Pontos / 1000;
        }
    }
}

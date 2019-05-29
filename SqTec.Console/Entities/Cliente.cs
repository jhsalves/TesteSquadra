using SqTec.Spec.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqTec.Console.Entities
{
    class Cliente : ICliente
    {
        public Guid IdentificadorERP { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Regiao { get; set; }

        public int Pontos { get; set; }
    }
}

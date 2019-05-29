using SqTec.Console.Entities;
using SqTec.Spec.Entities;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

namespace SqTec.Console.Services
{
    class ClienteService : IClienteService
    {
        private readonly IConfigService _configService;
        private string _arquivoDadosBackup { get; set; }
        public ClienteService(IConfigService configService)
        {
            _configService = configService;
        }

        public void Atualizar(ICliente cliente)
        {
            throw new NotImplementedException();
        }

        public void BeginTransaction()
        {
            _arquivoDadosBackup = LerArquivo(Consts.CaminhoArquivoDados);
        }

        private string LerArquivo(string caminho)
        {
            var filePath = _configService.ObterConfiguracao<string>(caminho);
            var conteudoArquivo = string.Empty;
            if (File.Exists(filePath))
            {
                conteudoArquivo = File.ReadAllText(filePath);
            }
            else
            {
                File.Create(filePath);
            }
            return conteudoArquivo;
        }

        private void GravarArquivo(string nome, string conteudo)
        {
            var filePath = _configService.ObterConfiguracao<string>(nome);
            File.WriteAllText(filePath, conteudo);
        }

        public int CalcularIdade(ICliente cliente)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void Inserir(ICliente cliente)
        {
            var listaClientes = ClientesColecao().ToList();
            listaClientes.Add(cliente);
            var conteudoArquivo = JsonConvert.SerializeObject(listaClientes);
            GravarArquivo(Consts.CaminhoArquivoDados, conteudoArquivo);
        }

        public IEnumerable<ICliente> Listar()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ICliente> ObterClientesDeTxt(string caminho)
        {
            var clientesLines = File.ReadAllLines(caminho);
            var listaClientes = new List<ICliente>();
            foreach (var clienteText in clientesLines)
            {
                var partesCliente = clienteText.Split(';');
                var cliente = new Cliente();
                cliente.IdentificadorERP = Guid.Parse(partesCliente[0]);
                cliente.Nome = partesCliente[1];
                cliente.DataNascimento = DateTime.Parse(partesCliente[2]);
                cliente.Regiao = partesCliente[3];
                cliente.Pontos = Convert.ToInt32(partesCliente[4]);
                listaClientes.Add(cliente);
            }
            return listaClientes;
        }

        private IEnumerable<ICliente> ClientesColecao()
        {
            var conteudoArquivoDados = LerArquivo(Consts.CaminhoArquivoDados);
            if (conteudoArquivoDados == string.Empty)
            {
                return new List<ICliente>();
            }
            return JsonConvert.DeserializeObject<List<Cliente>>(conteudoArquivoDados);
        }

        public ICliente ObterPorId(Guid identificadorErp)
        {
            var listaClientes = ClientesColecao();
            return listaClientes.FirstOrDefault(x => x.IdentificadorERP.Equals(identificadorErp));
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}

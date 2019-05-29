using SqTec.Console.Entities;
using SqTec.Spec.Entities;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using SqTec.Spec.Exceptions;

namespace SqTec.Console.Services
{
    class ClienteService : IClienteService
    {
        private readonly IConfigService _configService;
        private string _arquivoDadosBackup { get; set; }
        private string _conteudoDados { get; set; }
        public ClienteService(IConfigService configService)
        {
            _configService = configService;
        }

        public void Atualizar(ICliente cliente)
        {
            var clientes = ClientesColecao().ToList();
            var clienteExistente = clientes.FirstOrDefault(x => x.IdentificadorERP.Equals(cliente.IdentificadorERP));
            if (clienteExistente != null)
            {
                clienteExistente.DataNascimento = cliente.DataNascimento;
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Pontos = cliente.Pontos;
                clienteExistente.Regiao = cliente.Regiao;
            }
            _conteudoDados = JsonConvert.SerializeObject(clientes);
        }

        public void BeginTransaction()
        {
            _conteudoDados = LerArquivo(Consts.CaminhoArquivoDados);
            _arquivoDadosBackup = _conteudoDados;
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
                var fileStream = File.Create(filePath);
                fileStream.Close();
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
            return DateTime.Now.Year - cliente.DataNascimento.Year;
        }

        public void Commit()
        {
            GravarArquivo(Consts.CaminhoArquivoDados, _conteudoDados);
        }

        public void Inserir(ICliente cliente)
        {
            var listaClientes = ClientesColecao().ToList<ICliente>();
            listaClientes.Add(cliente);
            _conteudoDados = JsonConvert.SerializeObject(listaClientes);
        }

        public IEnumerable<ICliente> Listar()
        {
            return ClientesColecao();
        }

        public IEnumerable<ICliente> ObterClientesDeTxt(string caminho)
        {
            var clientesLines = File.ReadAllLines(caminho);
            var listaClientes = new List<ICliente>();
            foreach (var clienteText in clientesLines)
            {
                var partesCliente = clienteText.Split(';');
                var cliente = new Cliente();
                try
                {
                    cliente.IdentificadorERP = Guid.Parse(partesCliente[0]);
                    cliente.Nome = partesCliente[1];
                    cliente.DataNascimento = DateTime.Parse(partesCliente[2]);
                    cliente.Regiao = partesCliente[3];
                    cliente.Pontos = Convert.ToInt32(partesCliente[4]);
                    listaClientes.Add(cliente);
                }
                catch
                {
                    throw new LinhaInvalidaException(clienteText);
                }
            }
            return listaClientes;
        }

        private IEnumerable<Cliente> ClientesColecao()
        {
            if (string.IsNullOrEmpty(_conteudoDados))
            {
                return new List<Cliente>();
            }
            return JsonConvert.DeserializeObject<List<Cliente>>(_conteudoDados);
        }

        public ICliente ObterPorId(Guid identificadorErp)
        {
            var listaClientes = ClientesColecao();
            return listaClientes.FirstOrDefault(x => x.IdentificadorERP.Equals(identificadorErp));
        }

        public void Rollback()
        {
            _conteudoDados = _arquivoDadosBackup;
            GravarArquivo(Consts.CaminhoArquivoDados, _conteudoDados);
        }
    }
}

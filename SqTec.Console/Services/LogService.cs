using SqTec.Spec.Services;
using System;
using System.IO;

namespace SqTec.Console.Services
{
    class LogService : ILogService
    {
        private readonly IConfigService _configService;

        public LogService(IConfigService configService)
        {
            _configService = configService;
        }

        public void Log(string mensagem)
        {
            EscreverLog(mensagem);
        }

        private void EscreverLog(string mensagem)
        {
            var caminhoArquivoLog = _configService.ObterConfiguracao<string>(Consts.CaminhoArquivoLog);
            File.WriteAllText(caminhoArquivoLog, String.Format("{0} | {1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), mensagem));
        }

        public void Log(Exception ex)
        {
            EscreverLog(ex.Message);
        }
    }
}

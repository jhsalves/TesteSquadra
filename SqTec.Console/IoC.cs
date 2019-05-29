using SqTec.Console.Services;
using SqTec.Spec.IoC;
using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqTec.Console
{
    public static class IoC
    {
        public static Container ObterContainer()
        {
            var container = Container.Inicializar();

            // Registre suas dependências aqui...

            container.Registrar<IClienteService, ClienteService>();
            container.Registrar<ILogService, LogService>();
            container.Registrar<IExibicaoService, ExibicaoService>();
            container.Registrar<IPremiacaoService, PremiacaoService>();
            container.Registrar<IConfigService, ConfigService>();
            container.Registrar<Sistema>();

            return container;
        }
    }
}

using SqTec.Spec.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqTec.Console.Services
{
    class ConfigService : IConfigService
    {
        public T ObterConfiguracao<T>(string chave)
        {
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[chave], typeof(T));
        }
    }
}

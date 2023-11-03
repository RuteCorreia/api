using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades.Cadastros.Aplicacao
{
    public enum AplicacaoEnum
    {
        ProntoPraEnvio = 1,
        Tentativa = 2,
        PendenteInformacoes = 3,
        EnviadoEFinalizado = 4
    }
}

using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Cadastros.Precificacao;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Precificacao
{
    public class PrecificacaoService : BaseService<Entities.Entidades.Cadastros.Precificacao.Precificacao>, IPrecificacaoService
    {
        public PrecificacaoService(IBaseRepository<Entities.Entidades.Cadastros.Precificacao.Precificacao> baseRepository) : base(baseRepository)
        {
        }
    }
}

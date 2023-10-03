using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Aplicacao
{
    public class AplicacaoService : BaseService<Entities.Entidades.Cadastros.Aplicacao.Aplicacao>, IAplicacaoService
    {
        public AplicacaoService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.Aplicacao> baseRepository) : base(baseRepository)
        {
        }
    }
}

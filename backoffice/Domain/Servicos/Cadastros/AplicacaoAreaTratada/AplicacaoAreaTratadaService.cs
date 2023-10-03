using Domain.Interfaces.Cadastros.Aplicacao;
using Domain.Interfaces.Cadastros.AplicacaoAreaTratada;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AplicacaoAreaTratada
{
    public class AplicacaoAreaTratadaService : BaseService<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>, IAplicacaoAreaTratadaService
    {
        public AplicacaoAreaTratadaService(IBaseRepository<Entities.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> baseRepository) : base(baseRepository)
        {
        }
    }
}

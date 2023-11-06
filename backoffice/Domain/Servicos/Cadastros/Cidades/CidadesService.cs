using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cidades;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Cidades
{
    public class CidadesService : BaseService<Domain.Entidades.Cadastros.Cidades.Cidades>, ICidadeService
    {
        public CidadesService(IBaseRepository<Domain.Entidades.Cadastros.Cidades.Cidades> baseRepository) : base(baseRepository)
        {
        }
    }
}

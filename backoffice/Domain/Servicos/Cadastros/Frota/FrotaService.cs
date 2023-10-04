using Domain.Interfaces.Cadastros.Executor;
using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Frota
{
    public class FrotaService : BaseService<Entities.Entidades.Cadastros.Frota.Frota>, IFrotaService
    {
        private readonly IFrotaRepository _frotaRepository;

        public FrotaService(IFrotaRepository frotaRepository) : base(frotaRepository)
        {
            _frotaRepository = frotaRepository;
        }

        public Entities.Entidades.Cadastros.Frota.Frota BuscarPorId(int? Id)
        {
            var obj = _frotaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Frota.Frota> ListarFrotas()
        {
            var obj = _frotaRepository.ListarFrotas();
            return obj;
        }
    }
}

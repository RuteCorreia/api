using Domain.Interfaces.Cadastros.Frota;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Piloto
{
    public class PilotoService : BaseService<Entities.Entidades.Cadastros.Pilotos.Piloto>, IPilotoService
    {
        private readonly IPilotoRepository _pilotoRepository;

        public PilotoService(IPilotoRepository pilotoRepository) : base(pilotoRepository)
        {
            _pilotoRepository = pilotoRepository;
        }

        public Entities.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id)
        {
            var obj = _pilotoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos()
        {
            var obj = _pilotoRepository.ListarPilotos();
            return obj;
        }
    }
}

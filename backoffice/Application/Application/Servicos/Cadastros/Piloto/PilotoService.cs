using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Piloto;

namespace Application.Application.Servicos.Cadastros.Piloto
{
    public class PilotoService : BaseService<Domain.Entidades.Cadastros.Pilotos.Piloto>, IPilotoService
    {
        private readonly IPilotoRepository _pilotoRepository;

        public PilotoService(IPilotoRepository pilotoRepository) : base(pilotoRepository)
        {
            _pilotoRepository = pilotoRepository;
        }

        public Domain.Entidades.Cadastros.Pilotos.Piloto BuscarPorId(int? Id)
        {
            var obj = _pilotoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Pilotos.Piloto> ListarPilotos()
        {
            var obj = _pilotoRepository.ListarPilotos();
            return obj;
        }
    }
}

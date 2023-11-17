using Domain.Interfaces.Cadastros.ControleDeFrota;
using Application.Application.Servicos.Genericos;

namespace Application.Application.Servicos.Cadastros.ControleDeFrota
{
    public class ControleDeFrotaService : BaseService<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota>, IControleDeFrotaService
    {
        private readonly IControleDeFrotaRepository _controleDeFrotaRepository;

        public ControleDeFrotaService(IControleDeFrotaRepository controleDeFrotaRepository) : base(controleDeFrotaRepository)
        {
            _controleDeFrotaRepository = controleDeFrotaRepository;
        }

        public Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota BuscarPorId(int? Id)
        {
            var obj = _controleDeFrotaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Controle_De_Frota.ControleDeFrota> ListarTodosControlesDeFrota()
        {
            var obj = _controleDeFrotaRepository.ListarTodosControlesDeFrota();
            return obj;
        }
    }
}

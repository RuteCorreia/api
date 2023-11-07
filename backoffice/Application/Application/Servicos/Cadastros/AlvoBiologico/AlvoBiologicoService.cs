using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.AlvoBiologico;

namespace Application.Application.Servicos.Cadastros.AlvoBiologico
{
    public class AlvoBiologicoService : BaseService<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>, IAlvoBiologicoService
    {
        private readonly IAlvoBiologicoRepository _alvoBiologicoRepository;

        public AlvoBiologicoService(IAlvoBiologicoRepository alvoBiologicoRepository) : base(alvoBiologicoRepository)
        {
            _alvoBiologicoRepository = alvoBiologicoRepository;
        }

        public Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico BuscarPorId(int? Id)
        {
            var obj = _alvoBiologicoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> ListarTodosAlvosBiologicos()
        {
            var obj = _alvoBiologicoRepository.ListarTodosAlvosBiologicos();
            return obj;
        }
    }
}

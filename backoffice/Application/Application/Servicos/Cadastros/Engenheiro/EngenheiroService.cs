using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Engenheiro;

namespace Application.Application.Servicos.Cadastros.Engenheiro
{
    public class EngenheiroService : BaseService<Domain.Entidades.Cadastros.Engenheiro.Engenheiro>, IEngenheiroService
    {
        private readonly IEngenheiroRepository _engenheiroRepository;

        public EngenheiroService(IEngenheiroRepository engenheiroRepository) : base(engenheiroRepository)
        {
            _engenheiroRepository = engenheiroRepository;
        }

        public Domain.Entidades.Cadastros.Engenheiro.Engenheiro BuscarPorId(int? Id)
        {
            var obj = _engenheiroRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Engenheiro.Engenheiro> ListarTodosEngenheiros()
        {
            var obj = _engenheiroRepository.ListarTodosEngenheiros();
            return obj;
        }
    }
}

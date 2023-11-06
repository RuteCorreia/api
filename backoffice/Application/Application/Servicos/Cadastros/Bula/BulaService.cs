using Domain.Interfaces.Cadastros.Bula;
using Application.Application.Servicos.Genericos;

namespace Application.Application.Servicos.Cadastros.Bula
{
    public class BulaService : BaseService<Domain.Entidades.Cadastros.Empresa.Bula>, IBulaService
    {
        private readonly IBulaRepository _bulaRepository;

        public BulaService(IBulaRepository bulaRepository) : base(bulaRepository)
        {
            _bulaRepository = bulaRepository;
        }

        public Domain.Entidades.Cadastros.Empresa.Bula BuscarPorId(int? Id)
        {
            var obj = _bulaRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Empresa.Bula> ListarTodasBulas()
        {
            var obj = _bulaRepository.ListarTodasBulas();
            return obj;
        }
    }
}

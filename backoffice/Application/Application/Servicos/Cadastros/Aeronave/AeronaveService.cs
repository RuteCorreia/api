using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.Aeronave;

namespace Application.Application.Servicos.Cadastros.Aeronave
{
    public class AeronaveService : BaseService<Domain.Entidades.Cadastros.Aeronave.Aeronave>, IAeronaveService
    {
        private readonly IAeronaveRepository _aeronaveRepository;

        //public AeronaveService(IBaseRepository<Entities.Entidades.Cadastros.Aeronaves.Aeronave> baseRepository) : base(baseRepository)
        //{
        //}

        public AeronaveService(IAeronaveRepository aeronaveRepository) : base(aeronaveRepository)
        {
            _aeronaveRepository = aeronaveRepository;
        }


        public Domain.Entidades.Cadastros.Aeronave.Aeronave BuscarPorId(int? Id)
        {
            var obj = _aeronaveRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.Aeronave.Aeronave> ListarTodasAeronaves()
        {
            var obj = _aeronaveRepository.ListarTodasAeronaves();
            return obj;
        }
    }
}

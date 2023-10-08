using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using Entities.Entidades.Cadastros.Aeronaves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Aeronave
{
    public class AeronaveService : BaseService<Entities.Entidades.Cadastros.Aeronaves.Aeronave>, IAeronaveService
    {
        private readonly IAeronaveRepository _aeronaveRepository;

        //public AeronaveService(IBaseRepository<Entities.Entidades.Cadastros.Aeronaves.Aeronave> baseRepository) : base(baseRepository)
        //{
        //}

        public AeronaveService(IAeronaveRepository aeronaveRepository) : base(aeronaveRepository)
        {
            _aeronaveRepository = aeronaveRepository;
        }


        public Entities.Entidades.Cadastros.Aeronaves.Aeronave BuscarPorId(int? Id)
        {
            var obj = _aeronaveRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Aeronaves.Aeronave> ListarTodasAeronaves()
        {
            var obj = _aeronaveRepository.ListarTodasAeronaves();
            return obj;
        }
    }
}

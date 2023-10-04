using Domain.Interfaces.Cadastros.ControleDeFrota;
using Domain.Interfaces.Cadastros.Engenheiro;
using Domain.Interfaces.Cadastros.Piloto;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.Engenheiro
{
    public class EngenheiroService : BaseService<Entities.Entidades.Cadastros.Engenheiros.Engenheiro>, IEngenheiroService
    {
        private readonly IEngenheiroRepository _engenheiroRepository;

        public EngenheiroService(IEngenheiroRepository engenheiroRepository) : base(engenheiroRepository)
        {
            _engenheiroRepository = engenheiroRepository;
        }

        public Entities.Entidades.Cadastros.Engenheiros.Engenheiro BuscarPorId(int? Id)
        {
            var obj = _engenheiroRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Engenheiros.Engenheiro> ListarTodosEngenheiros()
        {
            var obj = _engenheiroRepository.ListarTodosEngenheiros();
            return obj;
        }
    }
}

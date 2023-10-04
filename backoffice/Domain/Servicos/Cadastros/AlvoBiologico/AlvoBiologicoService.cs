using Domain.Interfaces.Cadastros.Aeronave;
using Domain.Interfaces.Cadastros.AlvoBiologico;
using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.AlvoBiologico
{
    public class AlvoBiologicoService : BaseService<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>, IAlvoBiologicoService
    {
        private readonly IAlvoBiologicoRepository _alvoBiologicoRepository;

        public AlvoBiologicoService(IAlvoBiologicoRepository alvoBiologicoRepository) : base(alvoBiologicoRepository)
        {
            _alvoBiologicoRepository = alvoBiologicoRepository;
        }

        public Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico BuscarPorId(int? Id)
        {
            var obj = _alvoBiologicoRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> ListarTodosAlvosBiologicos()
        {
            var obj = _alvoBiologicoRepository.ListarTodosAlvosBiologicos();
            return obj;
        }
    }
}

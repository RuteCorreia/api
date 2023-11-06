using Domain.Interfaces.Cadastros.Bula;
using Domain.Interfaces.Cadastros.Cliente;
using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.CombateIncendio
{
    public class CombateIncendioService : BaseService<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>, ICombateIncendioService
    {
        private readonly ICombateIncendioRepository _combateIncendioRepository;

        public CombateIncendioService(ICombateIncendioRepository combateIncendioRepository) : base(combateIncendioRepository)
        {
            _combateIncendioRepository = combateIncendioRepository;

        }

        public Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio BuscarPorId(int? Id)
        {
            var obj = _combateIncendioRepository.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> ListarTodosCombatesIncendio()
        {
            var obj = _combateIncendioRepository.ListarTodosCombatesIncendio();
            return obj;
        }
    }
}

using Domain.Interfaces.Cadastros.CombateIncendio;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;
using Domain.Interfaces.Genericos;
using Domain.Servicos.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos.Cadastros.CombateIncendioDecolagemPouso
{
    public class CombateIncendioDecolagemPousoService : BaseService<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>, ICombateIncendioDecolagemPousoService
    {
        private readonly ICombateIncendioDecolagemPousoRepository _combateIncendioDecolagemPouso;

        public CombateIncendioDecolagemPousoService(ICombateIncendioDecolagemPousoRepository combateIncendioDecolagemPousoRepository) : base(combateIncendioDecolagemPousoRepository)
        {
            _combateIncendioDecolagemPouso = combateIncendioDecolagemPousoRepository;
        }

        public Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso BuscarPorId(int? Id)
        {
            var obj = _combateIncendioDecolagemPouso.BuscarPorId(Id);
            return obj;
        }

        public List<Entities.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> ListarTodosCombatesIncendioDecolagemPouso()
        {
            var obj = _combateIncendioDecolagemPouso.ListarTodosCombatesIncendioDecolagemPouso();
            return obj;
        }
    }
}

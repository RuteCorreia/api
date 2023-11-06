using Application.Application.Servicos.Genericos;
using Domain.Interfaces.Cadastros.CombateIncendioDecolagemPouso;

namespace Application.Application.Servicos.Cadastros.CombateIncendioDecolagemPouso
{
    public class CombateIncendioDecolagemPousoService : BaseService<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso>, ICombateIncendioDecolagemPousoService
    {
        private readonly ICombateIncendioDecolagemPousoRepository _combateIncendioDecolagemPouso;

        public CombateIncendioDecolagemPousoService(ICombateIncendioDecolagemPousoRepository combateIncendioDecolagemPousoRepository) : base(combateIncendioDecolagemPousoRepository)
        {
            _combateIncendioDecolagemPouso = combateIncendioDecolagemPousoRepository;
        }

        public Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso BuscarPorId(int? Id)
        {
            var obj = _combateIncendioDecolagemPouso.BuscarPorId(Id);
            return obj;
        }

        public List<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendioDecolagemPouso> ListarTodosCombatesIncendioDecolagemPouso()
        {
            var obj = _combateIncendioDecolagemPouso.ListarTodosCombatesIncendioDecolagemPouso();
            return obj;
        }
    }
}

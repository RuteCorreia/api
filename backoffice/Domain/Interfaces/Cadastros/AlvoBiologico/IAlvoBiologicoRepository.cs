using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AlvoBiologico;

public interface IAlvoBiologicoRepository : IBaseRepository<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico>
{
    Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Alvo_Biologico.AlvoBiologico> ListarTodosAlvosBiologicos();
}

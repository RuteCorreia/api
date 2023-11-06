using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoLog;

public interface IAplicacaoLogRepository : IBaseRepository<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoLog> ListarTodasAplicacoesLog();
}

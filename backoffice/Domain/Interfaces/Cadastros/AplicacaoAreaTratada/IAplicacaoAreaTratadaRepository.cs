using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoAreaTratada;

public interface IAplicacaoAreaTratadaRepository : IBaseRepository<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoAreaTratada> ListarTodasAplicacoesAreaTratadas();
}

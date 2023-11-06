using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aplicacao;

public interface IAplicacaoRepository : IBaseRepository<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>
{
    Domain.Entidades.Cadastros.Aplicacao.Aplicacao BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.Aplicacao> ListarTodasAplicacoes();
}

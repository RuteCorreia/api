using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Aplicacao;

public interface IAplicacaoService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.Aplicacao>
{
    Domain.Entidades.Cadastros.Aplicacao.Aplicacao BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.Aplicacao> ListarTodasAplicacoes();
}

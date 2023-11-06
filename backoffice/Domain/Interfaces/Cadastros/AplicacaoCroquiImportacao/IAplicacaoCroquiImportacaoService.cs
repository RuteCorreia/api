using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCroquiImportacao;

public interface IAplicacaoCroquiImportacaoService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroquiImportacao> ListarTodasAplicacoesCroquiImportacoes();
}

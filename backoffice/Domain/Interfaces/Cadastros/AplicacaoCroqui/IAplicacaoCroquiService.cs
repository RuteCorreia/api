using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCroqui;

public interface IAplicacaoCroquiService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCroqui> ListarTodasAplicacoesCroqui();
}

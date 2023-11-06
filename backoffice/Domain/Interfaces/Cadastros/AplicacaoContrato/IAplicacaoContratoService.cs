using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoContrato;

public interface IAplicacaoContratoService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoContrato> ListarTodasAplicacoesContrato();
}

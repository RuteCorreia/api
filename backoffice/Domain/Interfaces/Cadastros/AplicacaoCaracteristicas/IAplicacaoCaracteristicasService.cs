using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.AplicacaoCaracteristicas;

public interface IAplicacaoCaracteristicasService : IBaseService<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas>
{
    Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas BuscarPorId(int? Id);
    List<Domain.Entidades.Cadastros.Aplicacao.AplicacaoCaracteristicas> ListarTodasAplicacoesCaracteristicas();
}

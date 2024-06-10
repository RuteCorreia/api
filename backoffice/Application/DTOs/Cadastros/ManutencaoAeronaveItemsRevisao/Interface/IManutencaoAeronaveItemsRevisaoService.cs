using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.ViewModel;

namespace Application.DTOs.Cadastros.ManutencaoAeronaveItemsRevisao.Interface;

public interface IManutencaoAeronaveItemsRevisaoService
{
    Task<IEnumerable<ManutencaoAeronaveItemsRevisaoViewModel>> GetByIdAeronaveAsync(int id);

}

using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.TipoDeUnidade.ViewModel;

namespace Application.DTOs.Cadastros.TipoDeUnidade.Interface
{
    public interface ITipoDeUnidadeService
    {
        Task<IEnumerable<TipoDeUnidadeViewModel>> GetAllAsync();
    }
}

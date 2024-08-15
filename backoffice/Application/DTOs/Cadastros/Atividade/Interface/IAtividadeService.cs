using Application.DTOs.Cadastros.Atividade.ViewModel;

namespace Application.DTOs.Cadastros.Atividade.Interface
{
    public interface IAtividadeService
    {
        Task<AtividadeViewModel> GetAtividadeByFiltrosAsync(AtividadeFiltroViewModel atividadeFiltroViewModel, string idEmpresa);
    }
}

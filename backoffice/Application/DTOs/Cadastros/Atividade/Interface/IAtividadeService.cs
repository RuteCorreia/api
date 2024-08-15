using Application.DTOs.Cadastros.Atividade.ViewModel;

namespace Application.DTOs.Cadastros.Atividade.Interface
{
    public interface IAtividadeService
    {
        Task<AtividadeViewModel> GetAtividadeByPrefixoAsync(string prefixoAeronave);
        Task<AtividadeViewModel> GetAtividadeByPilotoAsync(string piloto);
        Task<AtividadeViewModel> GetAtividadeByExecutorAsync(string executor);
        Task<AtividadeViewModel> GetAtividadeByContratanteAsync(string contratante);
    }
}

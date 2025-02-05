using Application.DTOs.Cadastros.Sincronizacao.ViewModel;

namespace Application.DTOs.Cadastros.Sincronizacao.Interface;

public interface ISincronizacaoService 
{
    Task<SincronizacaoViewModel> GetAsync(string? idEmpresa, DateTime dataUltimaSincronizacao);
}
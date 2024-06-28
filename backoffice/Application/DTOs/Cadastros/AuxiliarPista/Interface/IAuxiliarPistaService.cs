using Application.DTOs.Cadastros.AuxiliarPista.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;

namespace Application.DTOs.Cadastros.AuxiliarPista.Interface
{
    public interface IAuxiliarPistaService
    {
        Task<int> AddAsync(AuxiliarPistaViewModel obj);
    }
}

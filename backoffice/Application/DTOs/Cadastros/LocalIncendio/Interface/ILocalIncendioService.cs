using Application.DTOs.Cadastros.LocalIncendio.ViewModel;

namespace Application.DTOs.Cadastros.LocalIncendio.Interface
{
    public interface ILocalIncendioService
    {
        Task<int> AddAsync(LocalIncendioViewModel obj);
    }
}

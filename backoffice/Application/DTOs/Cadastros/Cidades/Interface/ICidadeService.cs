using Application.DTOs.Cadastros.Cidades.ViewModel;
using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.Cidades
{
    public interface ICidadeService 
    {
        Task<IEnumerable<CidadeViewModel>> GetAllAsync();
    }
}

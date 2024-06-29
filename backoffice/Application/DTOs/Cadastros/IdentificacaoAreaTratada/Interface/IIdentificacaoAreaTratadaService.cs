using Application.DTOs.Cadastros.AplicacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.IdentificacaoAreaTratada.Interface
{
    public interface IIdentificacaoAreaTratadaService
    {
        Task<IEnumerable<IdentificacaoAreaTratadaViewModel>> GetAllAsync();

        Task<AreaTratadaViewModel> GetByIdAsync(int id);

        Task<int> AddAsync(AreaTratadaViewModel obj, string? idEmpresa);

        Task UpdateAsync(IdentificacaoAreaTratadaViewModel obj);

        Task DeleteAsync(int id);
    }
}

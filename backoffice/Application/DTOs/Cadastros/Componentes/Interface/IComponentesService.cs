using Application.DTOs.Cadastros.Combustivel.ViewModel;
using Application.DTOs.Cadastros.Componentes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Componentes.Interface
{
    public interface IComponentesService
    {
        Task<IEnumerable<ComponentesViewModel>> GetAllAsync(string? idEmpresa);

        Task<ComponentesViewModel> GetByIdAsync(int id);
        Task<IEnumerable<ComponentesViewModel>> GetByIdAeronaveAsync(int id);

        Task AddAsync(ComponentesViewModel obj, string? idEmpresa);

        Task UpdateAsync(ComponentesViewModel obj);

        Task DeleteAsync(int id);
    }
}

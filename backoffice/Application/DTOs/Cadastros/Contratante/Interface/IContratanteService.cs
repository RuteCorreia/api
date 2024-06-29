using Application.DTOs.Cadastros.Componentes.ViewModel;
using Application.DTOs.Cadastros.Contratante.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.Contratante.Interface
{
    public interface IContratanteService
    {
        Task<IEnumerable<ContratanteViewModel>> GetAllAsync();

        Task<ContratanteViewModel> GetByIdAsync(int id);

        Task<int> AddAsync(ContratanteViewModel obj, string? idEmpresa);

        Task UpdateAsync(ContratanteViewModel obj);

        Task DeleteAsync(int id);
    }
}

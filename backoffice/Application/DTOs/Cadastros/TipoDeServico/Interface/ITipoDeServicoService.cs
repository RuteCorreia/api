using Application.DTOs.Cadastros.TipoDeServico.ViewModel;
using Application.DTOs.Cadastros.Veiculante.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.TipoDeServico.Interface
{
    public interface ITipoDeServicoService
    {
        Task<IEnumerable<TipoDeServicoViewModel>> GetAllAsync();

        Task<TipoDeServicoViewModel> GetByIdAsync(int id);

        Task AddAsync(TipoDeServicoViewModel obj);

        Task UpdateAsync(TipoDeServicoViewModel obj);

        Task DeleteAsync(int id);
    }
}

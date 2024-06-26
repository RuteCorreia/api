using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.IdentificacaoAreaTratada.ViewModel;
using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
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

        Task<IdentificacaoAreaTratadaViewModel> GetByIdAsync(int id);

        Task<int> AddAsync(AreaTratadaViewModel obj);

        Task UpdateAsync(IdentificacaoAreaTratadaViewModel obj);

        Task DeleteAsync(int id);
    }
}

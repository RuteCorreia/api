using Application.DTOs.Cadastros.RelatorioAplicacao.ViewModel;
using Application.DTOs.Cadastros.SubMenu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.RelatorioAplicacao.Interface
{
    public interface IRelatorioAplicacaoService
    {
        Task<IEnumerable<RelatorioAplicacaoViewModel>> GetAllAsync();

        Task<RelatorioAplicacaoViewModel> GetByIdAsync(int id);

        Task AddAsync(RelatorioAplicacaoViewModel obj);

        Task UpdateAsync(RelatorioAplicacaoViewModel obj);

        Task DeleteAsync(int id);
    }
}

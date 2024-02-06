using Application.DTOs.Cadastros.BulaAplicacao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.BulaAplicacao.Interface
{
    public interface IBulaAplicacaoService
    {
        Task<IEnumerable<BulaAplicacaoViewModel>> GetAllAsync();

        Task<BulaAplicacaoViewModel> GetByIdAsync(int id);
        Task<List<BulaAplicacaoViewModel>> GetByIdBulaAsync(int id);

        Task AddAsync(BulaAplicacaoViewModel obj);

        Task UpdateAsync(BulaAplicacaoViewModel obj);

        Task DeleteAsync(int id);
    }
}

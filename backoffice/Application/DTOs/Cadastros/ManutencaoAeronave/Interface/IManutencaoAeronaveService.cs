using Application.DTOs.Cadastros.Frota.ViewModel;
using Application.DTOs.Cadastros.ManutencaoAeronave.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cadastros.ManutencaoAeronave.Interface
{
    public interface IManutencaoAeronaveService
    {
        Task<IEnumerable<ManutencaoAeronaveViewModel>> GetAllAsync();

        Task<ManutencaoAeronaveViewModel> GetByIdAsync(int id);

        Task AddAsync(ManutencaoAeronaveViewModel obj);

        Task UpdateAsync(ManutencaoAeronaveViewModel obj);

        Task DeleteAsync(int id);
    }
}

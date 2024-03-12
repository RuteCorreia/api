using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.ManutencaoAeronave
{
    public interface IManutencaoAeronaveRepository
    {
        Task AddAsync(Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj);
        Task UpdateAsync(Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave>> GetAllAsync();
        Task<Entidades.Cadastros.ManutencaoAeronave.ManutencaoAeronave> GetByIdAsync(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Contratante
{
    public interface IContratanteRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.Contratante.Contratante obj);
        Task UpdateAsync(Entidades.Cadastros.Contratante.Contratante obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Contratante.Contratante>> GetAllAsync();
        Task<Entidades.Cadastros.Contratante.Contratante> GetByIdAsync(int? id);
    }
}

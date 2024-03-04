using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.Componentes
{
    public interface IComponenteRepository
    {
        Task AddAsync(Entidades.Cadastros.Componentes.Componentes obj);
        Task UpdateAsync(Entidades.Cadastros.Componentes.Componentes obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Componentes.Componentes>> GetAllAsync();
        Task<Entidades.Cadastros.Componentes.Componentes> GetByIdAsync(int id);
    }
}

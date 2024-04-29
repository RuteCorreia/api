using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.IdentificacaoAreaTratada
{
    public interface IIdentificacaoAreaTratadaRepository
    {
        Task AddAsync(Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj);
        Task UpdateAsync(Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada>> GetAllAsync();
        Task<Entidades.Cadastros.IdentificacaoAreaTratada.IdentificacaoAreaTratada> GetByIdAsync(int id);
    }
}

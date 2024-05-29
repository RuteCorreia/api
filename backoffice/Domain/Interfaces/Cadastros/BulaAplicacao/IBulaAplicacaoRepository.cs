using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.BulaAplicacao
{
    public interface IBulaAplicacaoRepository
    {
        Task AddAsync(Entidades.Cadastros.Empresa.BulaAplicacao obj);
        Task UpdateAsync(Entidades.Cadastros.Empresa.BulaAplicacao obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Empresa.BulaAplicacao>> GetAllAsync();
        Task<Entidades.Cadastros.Empresa.BulaAplicacao> GetByIdAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.Empresa.BulaAplicacao>> GetByIdBulaAsync(int id);
    }
}

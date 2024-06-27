using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.RelatorioAplicacao
{
    public interface IRelatorioAplicacaoRepository
    {
        Task<int> AddAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync();
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByPilotId(int pilotoId);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDateAsync(DateTime date);
        Task<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id);
    }
}

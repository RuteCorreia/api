using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cadastros.RelatorioAplicacao
{
    public interface IRelatorioAplicacaoRepository
    {
        Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> AddAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj);
        Task UpdateAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync();
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByPilotId(int pilotoId);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllByIdEmpresaAsync(int idEmpresa);

        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataAlteracaoAsync(DateTime dataAlteracao, int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataCriacaoAsync(DateTime dataCriacao, int idEmpresa);
        Task<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id);
    }
}

using Domain.Entidades.Cadastros.Atividade;
using Domain.Entidades.Cadastros.Empresa;
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
        Task UpdateDataAlteracaoAsync(int? id);
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync();
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByIdsAsync(List<int> ids);
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByPilotId(int pilotoId);
        Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> ExportExcelAsync(int? id);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByIdsAsync(List<int> ids, int idEmpresa, int statusEnvio, int isMapa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllByIdEmpresaAsync(int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaAsync(int idEmpresa, int statusEnvio);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataAlteracaoAsync(DateTime dataAlteracao, int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataCriacaoAsync(DateTime dataCriacao, int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetNovosAsync(DateTime? dataCriacao, string userName, IEnumerable<string>? roleNames, int IdEmpresa);
        Task<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id);
        Task<IEnumerable<Atividade>> GetAtividadesByFiltrosAsync(AtividadeFiltro atividadeFiltro);
        Task CancelarAsync(Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao obj);
        Task UpdateIsMapaAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao relatorioExistente);
    }
}

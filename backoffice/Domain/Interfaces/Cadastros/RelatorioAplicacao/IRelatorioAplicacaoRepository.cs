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
        Task DeleteAsync(int id);
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllAsync();
        Task<IEnumerable<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByPilotId(int pilotoId);
        Task<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> ExportExcelAsync(int? id);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetAllByIdEmpresaAsync(int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaAsync(int idEmpresa, int statusEnvio);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetListByStatusMapaMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataAlteracaoAsync(DateTime dataAlteracao, int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetByDataCriacaoAsync(DateTime dataCriacao, int idEmpresa);
        Task<IEnumerable<Domain.Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao>> GetNovosAsync(DateTime? dataCriacao, string userName);
        Task<Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao> GetByIdAsync(int id);
        Task<List<Atividade>> GetAtividadeByPrefixoAsync(string prefixoAeronave);
        Task<List<Atividade>> GetAtividadeByPilotoAsync(string piloto);
        Task<List<Atividade>> GetAtividadeByExecutorAsync(string executor);
        Task<List<Atividade>> GetAtividadeByContratanteAsync(string contratante);
        Task UpdateIsMapaAsync(Entidades.Cadastros.RelatorioAplicacao.RelatorioAplicacao relatorioExistente);
    }
}

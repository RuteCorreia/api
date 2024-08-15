using Domain.Entidades.Cadastros.Atividade;
using Domain.Interfaces.Genericos;

namespace Domain.Interfaces.Cadastros.CombateIncendio;

public interface ICombateIncendioRepository
{
    Task<int> AddAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task<int> UpdateAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio obj);
    Task DeleteAsync(int id);
    Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetListByStatusAsync(int idEmpresa, int statusEnvio);
    Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetListByStatusMapaAsync(int idEmpresa, int statusEnvio);
    Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetListByStatusMapaMesAsync(int idEmpresa, int statusEnvio, DateTime primeiroDiaMes, DateTime ultimoDiaMes);
    Task<IEnumerable<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio>> GetAllAsync(DateTime? offsetDate, Guid idUser, string userName);
    Task<Entidades.Cadastros.CombateIncendio.CombateIncendio> GetByIdAsync(int id);
    Task<Domain.Entidades.Cadastros.CombateIncendio.CombateIncendio> ExportExcelAsync(int? id);
    Task<List<Atividade>> GetAtividadeByPrefixoAsync(string prefixoAeronave);
    Task<List<Atividade>> GetAtividadeByPilotoAsync(string piloto);
    Task<List<Atividade>> GetAtividadeByExecutorAsync(Guid idExecutor);
    Task<List<Atividade>> GetAtividadeByContratanteAsync(string cliente);
    Task UpdateIsMapaAsync(Entidades.Cadastros.CombateIncendio.CombateIncendio relatorioExistente);
}

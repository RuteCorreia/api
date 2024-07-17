using Domain.Interfaces.Cadastros.RelatorioManutencao;
using Helpers;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoRepository : IRelatorioManutencaoRepository
    {
        private readonly ContextBase _contextBase;

        public RelatorioManutencaoRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task AddAsync(Domain.Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao obj)
        {
            await _contextBase.AddAsync(obj);
            await _contextBase.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entityToRemove = await GetByIdAsync(id);
            if (!ObjectNullValidation.IsObjectNull(entityToRemove))
            {
                _contextBase.Remove(entityToRemove);
                await _contextBase.SaveChangesAsync();
            }
        }

        public async Task<Domain.Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioManutencao.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(Domain.Entidades.Cadastros.RelatorioManutencao.RelatorioManutencao obj)
        {
            var objeto = await _contextBase.RelatorioManutencao.FindAsync(obj.Id);

            if (objeto != null)
            {
                // Atualiza as propriedades do objeto encontrado
                objeto.IsMapa = obj.IsMapa;
                objeto.IdEmpresa = obj.IdEmpresa;
                objeto.NomeRelatorio = obj.NomeRelatorio;
                objeto.DataCriacao = obj.DataCriacao;
                objeto.DataAlteracao = obj.DataAlteracao;
                objeto.StatusEnvio = obj.StatusEnvio;
                objeto.IdAeronave = obj.IdAeronave;
                objeto.Horimetro = obj.Horimetro;

                // Atualiza o objeto no contexto
                _contextBase.RelatorioManutencao.Update(objeto);
                await _contextBase.SaveChangesAsync();
            }
            else
            {
                // Lidar com o caso em que o objeto não foi encontrado
                throw new KeyNotFoundException("Objeto não encontrado.");
            }
        }
    }
}

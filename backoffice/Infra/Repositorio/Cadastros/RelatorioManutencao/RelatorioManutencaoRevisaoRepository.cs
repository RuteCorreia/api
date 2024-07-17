using Domain.Entidades.Cadastros.RelatorioManutencao;
using Domain.Interfaces.Cadastros.RelatorioManutencao;
using Helpers;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoRevisaoRepository : IRelatorioManutencaoRevisaoRepository
    {
        private readonly ContextBase _contextBase;

        public RelatorioManutencaoRevisaoRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task AddAsync(RelatorioManutencaoRevisao obj)
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

        public async Task<RelatorioManutencaoRevisao> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioManutencaoRevisao.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(RelatorioManutencaoRevisao obj)
        {
            var objeto = await _contextBase.RelatorioManutencaoRevisao.FindAsync(obj.Id);

            if (objeto != null)
            {
                // Atualiza as propriedades do objeto encontrado
                objeto.IdEmpresa = obj.IdEmpresa;
                objeto.IdRelatorioManutencao = obj.IdRelatorioManutencao;
                objeto.IsSelected = obj.IsSelected;
                objeto.IdManutencaoAeronaveItemsRevisao = obj.IdManutencaoAeronaveItemsRevisao;

                // Atualiza o objeto no contexto
                _contextBase.RelatorioManutencaoRevisao.Update(objeto);
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

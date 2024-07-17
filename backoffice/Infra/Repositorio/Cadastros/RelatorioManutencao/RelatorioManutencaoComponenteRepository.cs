using Domain.Entidades.Cadastros.RelatorioManutencao;
using Domain.Interfaces.Cadastros.RelatorioManutencao;
using Helpers;
using Infra.Configuracao;

namespace Infra.Repositorio.Cadastros.RelatorioManutencao
{
    public class RelatorioManutencaoComponenteRepository : IRelatorioManutencaoComponenteRepository
    {
        private readonly ContextBase _contextBase;

        public RelatorioManutencaoComponenteRepository(ContextBase contextBase)
        {
            _contextBase = contextBase;
        }
        public async Task AddAsync(RelatorioManutencaoComponente obj)
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

        public async Task<RelatorioManutencaoComponente> GetByIdAsync(int id)
        {
            var obj = await _contextBase.RelatorioManutencaoComponente.FindAsync(id);
            return obj;
        }

        public async Task UpdateAsync(RelatorioManutencaoComponente obj)
        {
            var objeto = await _contextBase.RelatorioManutencaoComponente.FindAsync(obj.Id);

            if (objeto != null)
            {
                // Atualiza as propriedades do objeto encontrado
                objeto.IdEmpresa = obj.IdEmpresa;
                objeto.IdRelatorioManutencao = obj.IdRelatorioManutencao;
                objeto.IdComponente = obj.IdComponente;
                objeto.Observacao = obj.Observacao;
                objeto.IdManutencaoAeronaveItemsRevisao = obj.IdManutencaoAeronaveItemsRevisao;

                // Atualiza o objeto no contexto
                _contextBase.RelatorioManutencaoComponente.Update(objeto);
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
